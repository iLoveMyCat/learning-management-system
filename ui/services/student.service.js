// services/student.service.js
import { http } from "../core/http.js";

export class StudentService {
  #base = "/api/students";

  getAll() {
    return http.request(this.#base);
  }
}
