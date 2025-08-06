// pages/enrollments.js
import { EnrollmentService } from "../services/enrollment.service.js";
import { StudentService } from "../services/student.service.js";
import { CourseService } from "../services/course.service.js";

class EnrollmentsPage {
  constructor() {
    this.enrollmentService = new EnrollmentService();
    this.studentService = new StudentService();
    this.courseService = new CourseService();

    // UI refs
    this.$tbody = document.querySelector("#enrollments-tbody");
    this.$student = document.querySelector("#studentId");
    this.$course = document.querySelector("#courseId");
    this.$form = document.querySelector("#enrollment-form");
    this.$msg = document.querySelector("#enroll-message");
    this.$error = document.querySelector("#enroll-error");

    // events
    this.$form.addEventListener("submit", (e) => this.onSubmit(e));

    // init
    this.init();
  }

  async init() {
    await Promise.all([
      this.loadEnrollments(),
      this.loadStudents(),
      this.loadCourses(),
    ]);
  }

  async loadEnrollments() {
    try {
      const items = await this.enrollmentService.getAll();
      this.renderTable(items);
    } catch (err) {
      console.error(err);
      this.renderTable([]);
      this.setError("Failed to load enrollments.");
    }
  }

  renderTable(items) {
    this.$tbody.innerHTML = "";
    if (!Array.isArray(items) || items.length === 0) {
      this.$tbody.innerHTML = `<tr><td colspan="2">No enrollments yet.</td></tr>`;
      return;
    }
    for (const e of items) {
      const tr = document.createElement("tr");
      tr.innerHTML = `
        <td>${this.escape(
          `${e.studentFirstName ?? ""} ${e.studentLastName ?? ""}`.trim()
        )}</td>
        <td>${this.escape(e.courseTitle ?? "")}</td>
      `;
      this.$tbody.appendChild(tr);
    }
  }

  async loadStudents() {
    try {
      const students = await this.studentService.getAll();
      this.$student.innerHTML = `<option value="">-- select --</option>`;
      for (const s of students) {
        const opt = document.createElement("option");
        opt.value = s.id;
        opt.textContent = `${s.firstName} ${s.lastName}`;
        this.$student.appendChild(opt);
      }
    } catch (err) {
      console.error(err);
      this.setError("Failed to load students.");
    }
  }

  async loadCourses() {
    try {
      const courses = await this.courseService.getAll();
      this.$course.innerHTML = `<option value="">-- select --</option>`;
      for (const c of courses) {
        const opt = document.createElement("option");
        opt.value = c.id;
        opt.textContent = c.title;
        this.$course.appendChild(opt);
      }
    } catch (err) {
      console.error(err);
      this.setError("Failed to load courses.");
    }
  }

  async onSubmit(e) {
    e.preventDefault();
    this.$msg.textContent = "";
    this.$error.textContent = "";

    const studentId = this.$student.value;
    const courseId = this.$course.value;

    if (!studentId || !courseId) {
      this.setError("Please select a student and a course.");
      return;
    }

    try {
      await this.enrollmentService.enroll({ studentId, courseId });
      this.$msg.textContent = "Enrollment created.";
      this.$form.reset();
      await this.loadEnrollments();
    } catch (err) {
      console.error(err);
      this.setError("Enrollment failed.");
    }
  }

  setError(msg) {
    this.$error.textContent = msg || "";
  }

  escape(s) {
    return String(s)
      .replaceAll("&", "&amp;")
      .replaceAll("<", "&lt;")
      .replaceAll(">", "&gt;");
  }
}

// bootstrap
window.addEventListener("DOMContentLoaded", () => new EnrollmentsPage());
