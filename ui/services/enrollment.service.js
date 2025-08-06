// services/enrollment.service.js
import { http } from "../core/http.js";

export class EnrollmentService {
  #base = "/api/enrollments";

  getAll() {
    return http.request(this.#base);
  }

  enroll(dto) {
    // { studentId, courseId }
    return http.request(this.#base, { method: "POST", body: dto });
  }

  getSummary() {
    return http.request(`${this.#base}/enrollment-summary`);
  }
}
