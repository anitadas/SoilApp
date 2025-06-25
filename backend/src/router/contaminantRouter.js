import express from "express";
import contaminantController from "../modules/contaminant/contaminantController.js";

const router = express.Router();

router.get("/", contaminantController.getAll);
router.get("/:id", contaminantController.getById);
router.post("/", contaminantController.create);
router.put("/:id", contaminantController.update);
router.delete("/:id", contaminantController.remove);

export default router;
