// services/course.service.js
import { http } from "../core/http.js";

export class CourseService {
  #base = "/api/courses";

  getAll() {
    return http.request(this.#base);
  }

  getById(id) {
    return http.request(`${this.#base}/${id}`);
  }

  create(dto) {
    return http.request(this.#base, { method: "POST", body: dto });
  }

  update(id, dto) {
    return http.request(`${this.#base}/${id}`, { method: "PUT", body: dto });
  }

  delete(id) {
    return http.request(`${this.#base}/${id}`, { method: "DELETE" });
  }
}
