import express from "express";
import pathwayController from "../modules/pathway/pathwayController.js";

const router = express.Router();

router.get("/", pathwayController.getAll);
router.get("/:id", pathwayController.getById);
router.post("/", pathwayController.create);
router.put("/:id", pathwayController.update);
router.delete("/:id", pathwayController.remove);

export default router;
