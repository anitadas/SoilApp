import express from "express";
import soilTypeController from "../modules/soilType/soilTypeController.js";

const router = express.Router();

router.get("/", soilTypeController.getAll);
router.get("/:id", soilTypeController.getById);
router.post("/", soilTypeController.create);
router.put("/:id", soilTypeController.update);
router.delete("/:id", soilTypeController.remove);

export default router;
