import { connection } from "../../config/dbconfig.js";

const measurementModel = {
  async getAll() {
    return new Promise((resolve, reject) => {
      connection.query("SELECT * FROM measurements", (err, results) => {
        if (err) reject(err);
        else resolve(results);
      });
    });
  },
  async getById(id) {
    return new Promise((resolve, reject) => {
      connection.query(
        "SELECT * FROM measurements WHERE id = ?",
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
        "INSERT INTO measurements SET ?",
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
        "UPDATE measurements SET ? WHERE id = ?",
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
        "DELETE FROM measurements WHERE id = ?",
        [id],
        (err, results) => {
          if (err) reject(err);
          else resolve({ id });
        }
      );
    });
  },
};

export default measurementModel;
