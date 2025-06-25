import express from "express";
import measurementController from "../modules/measurement/measurementController.js";

const router = express.Router();

router.get("/", measurementController.getAll);
router.get("/:id", measurementController.getById);
router.post("/", measurementController.create);
router.put("/:id", measurementController.update);
router.delete("/:id", measurementController.remove);

export default router;
