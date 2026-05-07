import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { AdvisorService } from './services/advisor.service';
import {
  AdvisorSummary,
  CarrierDistribution,
  MonthlyTrendEntry,
  Statement
} from './models/advisor.model';

interface AdvisorDashboardEntry {
  summary: AdvisorSummary;
  carrierDistribution: CarrierDistribution | null;
  monthlyTrend: MonthlyTrendEntry[];
  loadError: string | null;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  advisors: AdvisorDashboardEntry[] = [];
  loading = true;
  error: string | null = null;

  monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

  constructor(private advisorService: AdvisorService) {}

  ngOnInit(): void {
    this.advisorService.getSummaries().subscribe({
      next: (res) => {
        const summaries: AdvisorSummary[] = [res.advisor1, res.advisor2, res.advisor3]
          .filter(a => a.advisorId > 0);

        const detailRequests = summaries.map(s =>
          forkJoin({
            carriers: this.advisorService.getCarrierDistribution(s.advisorId),
            trend: this.advisorService.getMonthlyTrend(s.advisorId)
          })
        );

        forkJoin(detailRequests).subscribe({
          next: (details) => {
            this.advisors = summaries.map((s, i) => ({
              summary: s,
              carrierDistribution: details[i].carriers,
              monthlyTrend: this.aggregateTrend(details[i].trend),
              loadError: null
            }));
            this.loading = false;
          },
          error: (err) => {
            this.error = 'Failed to load advisor details.';
            this.loading = false;
          }
        });
      },
      error: (err) => {
        this.error = 'Failed to load advisor summaries.';
        this.loading = false;
      }
    });
  }

  private aggregateTrend(statements: Statement[]): MonthlyTrendEntry[] {
    const map = new Map<string, MonthlyTrendEntry>();
    for (const s of statements) {
      const d = new Date(s.statementDate);
      const year = d.getFullYear();
      const month = d.getMonth() + 1;
      const key = `${year}-${String(month).padStart(2, '0')}`;
      if (!map.has(key)) {
        map.set(key, {
          label: `${this.monthNames[month - 1]} ${year}`,
          year,
          month,
          totalCommissions: 0,
          statementCount: 0
        });
      }
      const entry = map.get(key)!;
      entry.totalCommissions += s.commissionAmount;
      entry.statementCount += 1;
    }
    return Array.from(map.values()).sort((a, b) =>
      a.year !== b.year ? a.year - b.year : a.month - b.month
    );
  }

  formatCurrency(val: number): string {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(val);
  }

  formatDate(dateStr: string): string {
    const d = new Date(dateStr);
    return `${this.monthNames[d.getMonth()]} ${d.getFullYear()}`;
  }

  formatMonthYear(year: number, month: number): string {
    return `${this.monthNames[month - 1]} ${year}`;
  }

  getMaxTrend(trend: MonthlyTrendEntry[]): number {
    return Math.max(...trend.map(t => t.totalCommissions), 1);
  }
}

