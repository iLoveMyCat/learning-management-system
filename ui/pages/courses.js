// pages/courses.js
import { CourseService } from "../services/course.service.js";

class CoursesPage {
  constructor() {
    this.courseService = new CourseService();

    // UI elements
    this.$tableBody = document.querySelector("#courses-tbody");
    this.$form = document.querySelector("#course-form");
    this.$title = document.querySelector("#course-title");
    this.$description = document.querySelector("#course-description");
    this.$submitBtn = document.querySelector("#course-submit");
    this.$cancelBtn = document.querySelector("#course-cancel");
    this.$formTitle = document.querySelector("#form-title");
    this.$error = document.querySelector("#course-error");

    // state
    this.editId = null;
    this.saving = false;

    // events
    this.$form.addEventListener("submit", (e) => this.onSubmit(e));
    this.$cancelBtn.addEventListener("click", () => this.resetForm());

    // init
    this.loadCourses();
  }

  async loadCourses() {
    try {
      const items = await this.courseService.getAll();
      this.renderTable(items);
    } catch (err) {
      console.error(err);
      this.renderTable([]);
      this.showError("Failed to load courses.");
    }
  }

  renderTable(items) {
    this.$tableBody.innerHTML = "";
    if (!Array.isArray(items) || items.length === 0) {
      this.$tableBody.innerHTML = `<tr><td colspan="3">No courses yet.</td></tr>`;
      return;
    }

    for (const c of items) {
      const tr = document.createElement("tr");
      tr.innerHTML = `
        <td>${this.escape(c.title)}</td>
        <td>${this.escape(c.description ?? "")}</td>
        <td class="actions">
          <button data-action="edit" data-id="${c.id}">Edit</button>
          <button data-action="delete" data-id="${c.id}">Delete</button>
        </td>
      `;
      tr.addEventListener("click", (e) => this.onRowAction(e));
      this.$tableBody.appendChild(tr);
    }
  }

  async onRowAction(e) {
    const btn = e.target.closest("button");
    if (!btn) return;
    const id = btn.dataset.id;

    if (btn.dataset.action === "edit") {
      await this.fillFormForEdit(id);
    } else if (btn.dataset.action === "delete") {
      if (confirm("Delete this course?")) {
        try {
          await this.courseService.delete(id);
          await this.loadCourses();
          this.resetFormIfDeleted(id);
        } catch (err) {
          console.error(err);
          this.showError("Failed to delete course.");
        }
      }
    }
  }

  async fillFormForEdit(id) {
    try {
      this.clearError();
      const c = await this.courseService.getById(id);
      if (!c) {
        this.showError("Course not found.");
        return;
      }
      this.editId = id;
      this.$title.value = c.title ?? "";
      this.$description.value = c.description ?? "";
      this.$submitBtn.textContent = "Update";
      this.$formTitle.textContent = "Edit Course";

      // UX: focus and scroll into view
      this.$title.focus({ preventScroll: true });
      this.$form.scrollIntoView({ behavior: "smooth", block: "start" });
    } catch (err) {
      console.error(err);
      this.showError("Failed to load course.");
    }
  }

  resetFormIfDeleted(id) {
    if (this.editId === id) this.resetForm();
  }

  resetForm() {
    this.editId = null;
    this.$form.reset();
    this.$submitBtn.textContent = "Create";
    this.$formTitle.textContent = "Add Course";
    this.clearError();
  }

  async onSubmit(e) {
    e.preventDefault();
    if (this.saving) return;

    const dto = {
      title: this.$title.value.trim(),
      description: this.$description.value.trim(),
    };

    if (!dto.title) {
      this.showError("Title is required.");
      return;
    }

    try {
      this.saving = true;
      this.$submitBtn.disabled = true;

      if (this.editId) {
        // EDIT → PUT /api/courses/{id}
        await this.courseService.update(this.editId, dto);
      } else {
        // CREATE → POST /api/courses
        await this.courseService.create(dto);
      }

      this.resetForm();
      await this.loadCourses();
    } catch (err) {
      console.error(err);
      this.showError("Save failed.");
    } finally {
      this.saving = false;
      this.$submitBtn.disabled = false;
    }
  }

  showError(msg) {
    this.$error.textContent = msg || "";
  }
  clearError() {
    this.$error.textContent = "";
  }

  escape(s) {
    return String(s)
      .replaceAll("&", "&amp;")
      .replaceAll("<", "&lt;")
      .replaceAll(">", "&gt;");
  }
}

window.addEventListener("DOMContentLoaded", () => new CoursesPage());
