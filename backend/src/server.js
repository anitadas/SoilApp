import express from "express";
import { config } from "dotenv";
import { connectToDatabase } from "./config/dbconfig.js";
import path from "./router.js";
import cors from "cors";
config();
export class server {
  app = express();
  constructor() {
    connectToDatabase();
    const PORT = process.env.PORT || 3000;
    this.app.use(express.json());
    this.app.use(cors());
    this.app.use(express.urlencoded({ extended: true }));
    this.app.use("/api", path());
    this.app.listen(PORT, () => {
      console.log(`Server is running on port ${PORT}`);
    });
  }
}
