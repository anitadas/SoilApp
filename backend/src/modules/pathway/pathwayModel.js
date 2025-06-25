import { connection } from "../../config/dbconfig.js";

const pathwayModel = {
  async getAll() {
    return new Promise((resolve, reject) => {
      connection.query("SELECT * FROM pathways", (err, results) => {
        if (err) reject(err);
        else resolve(results);
      });
    });
  },
  async getById(id) {
    return new Promise((resolve, reject) => {
      connection.query(
        "SELECT * FROM pathways WHERE id = ?",
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
      connection.query("INSERT INTO pathways SET ?", data, (err, results) => {
        if (err) reject(err);
        else resolve({ id: results.insertId, ...data });
      });
    });
  },
  async update(id, data) {
    return new Promise((resolve, reject) => {
      connection.query(
        "UPDATE pathways SET ? WHERE id = ?",
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
        "DELETE FROM pathways WHERE id = ?",
        [id],
        (err, results) => {
          if (err) reject(err);
          else resolve({ id });
        }
      );
    });
  },
};

export default pathwayModel;
