// pages/reports.js
import { EnrollmentService } from "../services/enrollment.service.js";

class ReportsPage {
  constructor() {
    this.enrollmentService = new EnrollmentService();
    this.$tbody = document.querySelector("#report-tbody");
    this.init();
  }

  async init() {
    try {
      const items = await this.enrollmentService.getSummary();
      this.render(items);
    } catch (err) {
      console.error(err);
      this.$tbody.innerHTML = `<tr><td colspan="2">Failed to load report.</td></tr>`;
    }
  }

  render(items) {
    this.$tbody.innerHTML = "";
    if (!Array.isArray(items) || items.length === 0) {
      this.$tbody.innerHTML = `<tr><td colspan="2">No data.</td></tr>`;
      return;
    }
    for (const r of items) {
      const tr = document.createElement("tr");
      tr.innerHTML = `
        <td>${this.escape(r.courseTitle ?? "")}</td>
        <td>${r.totalStudents ?? 0}</td>
      `;
      this.$tbody.appendChild(tr);
    }
  }

  escape(s) {
    return String(s)
      .replaceAll("&", "&amp;")
      .replaceAll("<", "&lt;")
      .replaceAll(">", "&gt;");
  }
}

window.addEventListener("DOMContentLoaded", () => new ReportsPage());
