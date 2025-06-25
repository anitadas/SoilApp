import express from "express";
import guidelineValueController from "../modules/guidelineValue/guidelineValueController.js";

const router = express.Router();

router.get("/", guidelineValueController.getAll);
router.get("/:id", guidelineValueController.getById);
router.post("/", guidelineValueController.create);
router.put("/:id", guidelineValueController.update);
router.delete("/:id", guidelineValueController.remove);

export default router;
