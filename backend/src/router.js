import { Router } from "express";
import pathwayRouter from "./router/pathwayRouter.js";
import measurementRouter from "./router/measurementRouter.js";
import guidelineValueRouter from "./router/guidelineValueRouter.js";
import contaminantRouter from "./router/contaminantRouter.js";
import soilTypeRouter from "./router/soilTypeRouter.js";

const path = () => {
  const router = Router();
  router.use("/pathways", pathwayRouter);
  router.use("/measurements", measurementRouter);
  router.use("/guideline-values", guidelineValueRouter);
  router.use("/contaminants", contaminantRouter);
  router.use("/soil-types", soilTypeRouter);
  return router;
};

export default path;
