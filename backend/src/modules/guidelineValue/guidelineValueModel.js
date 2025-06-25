import { connection } from "../../config/dbconfig.js";

const guidelineValueModel = {
  async getAll() {
    return new Promise((resolve, reject) => {
      connection.query(
        `SELECT gv.*, c.name AS contaminant_name, s.name AS soil_type_name, p.name AS pathway_name
         FROM guideline_values gv
         JOIN contaminants c ON gv.contaminant_id = c.id
         JOIN soil_types s ON gv.soil_type_id = s.id
         JOIN pathways p ON gv.pathway_id = p.id`,
        (err, results) => {
          if (err) reject(err);
          else resolve(results);
        }
      );
    });
  },
  async getById(id) {
    return new Promise((resolve, reject) => {
      connection.query(
        `SELECT gv.*, c.name AS contaminant_name, s.name AS soil_type_name, p.name AS pathway_name
         FROM guideline_values gv
         JOIN contaminants c ON gv.contaminant_id = c.id
         JOIN soil_types s ON gv.soil_type_id = s.id
         JOIN pathways p ON gv.pathway_id = p.id
         WHERE gv.id = ?`,
        [id],
        (err, results) => {
          if (err) reject(err);
          else resolve(results[0]);
        }
      );
    });
  },
  async create(data) {
    return new Promise((resolve, reject) => {
      connection.query(
        "INSERT INTO guideline_values SET ?",
        data,
        (err, results) => {
          if (err) reject(err);
          else resolve({ id: results.insertId, ...data });
        }
      );
    });
  },
  async update(id, data) {
    return new Promise((resolve, reject) => {
      connection.query(
        "UPDATE guideline_values SET ? WHERE id = ?",
        [data, id],
        (err, results) => {
          if (err) reject(err);
          else resolve({ id, ...data });
        }
      );
    });
  },
  async remove(id) {
    return new Promise((resolve, reject) => {
      connection.query(
        "DELETE FROM guideline_values WHERE id = ?",
        [id],
        (err, results) => {
          if (err) reject(err);
          else resolve({ id });
        }
      );
    });
  },
};

export default guidelineValueModel;
