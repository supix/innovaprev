/* tslint:disable */
/* eslint-disable */
// WARNING: This file was auto-generated with tsoa. Please do not modify it. Re-run tsoa to re-generate this file: https://github.com/lukeautry/tsoa
import type { TsoaRoute } from '@tsoa/runtime';
import {  fetchMiddlewares, ExpressTemplateService } from '@tsoa/runtime';
// WARNING: This file was auto-generated with tsoa. Please do not modify it. Re-run tsoa to re-generate this file: https://github.com/lukeautry/tsoa
import { ProjectsController } from './controllers/Windows';
import type { Request as ExRequest, Response as ExResponse, RequestHandler, Router } from 'express';



// WARNING: This file was auto-generated with tsoa. Please do not modify it. Re-run tsoa to re-generate this file: https://github.com/lukeautry/tsoa

const models: TsoaRoute.Models = {
    "WindowMaterialType": {
        "dataType": "refAlias",
        "type": {"dataType":"union","subSchemas":[{"dataType":"enum","enums":["CAS"]},{"dataType":"enum","enums":["F1A"]},{"dataType":"enum","enums":["F2A"]},{"dataType":"enum","enums":["FIX"]},{"dataType":"enum","enums":["FLD"]},{"dataType":"enum","enums":["FLS"]},{"dataType":"enum","enums":["FRO"]},{"dataType":"enum","enums":["PF1A"]},{"dataType":"enum","enums":["PF2A"]},{"dataType":"enum","enums":["PRT1A"]},{"dataType":"enum","enums":["PRT2A"]},{"dataType":"enum","enums":["SIL"]},{"dataType":"enum","enums":["SLA"]},{"dataType":"enum","enums":["SLF"]},{"dataType":"enum","enums":["SRAF"]},{"dataType":"enum","enums":["SRLA"]},{"dataType":"enum","enums":["VAS"]}],"validators":{}},
    },
    // WARNING: This file was auto-generated with tsoa. Please do not modify it. Re-run tsoa to re-generate this file: https://github.com/lukeautry/tsoa
};
const templateService = new ExpressTemplateService(models, {"noImplicitAdditionalProperties":"throw-on-extras","bodyCoercion":true});

// WARNING: This file was auto-generated with tsoa. Please do not modify it. Re-run tsoa to re-generate this file: https://github.com/lukeautry/tsoa




export function RegisterRoutes(app: Router) {

    // ###########################################################################################################
    //  NOTE: If you do not see routes for all of your controllers in this file, then you might not have informed tsoa of where to look
    //      Please look into the "controllerPathGlobs" config option described in the readme: https://github.com/lukeautry/tsoa
    // ###########################################################################################################


    
        const argsProjectsController_drawWindowImage: Record<string, TsoaRoute.ParameterSchema> = {
                height: {"in":"query","name":"height","required":true,"dataType":"double"},
                width: {"in":"query","name":"width","required":true,"dataType":"double"},
                materialType: {"in":"query","name":"materialType","required":true,"ref":"WindowMaterialType"},
                request: {"in":"request","name":"request","required":true,"dataType":"object"},
        };
        app.get('/api/windows/drawWindow',
            ...(fetchMiddlewares<RequestHandler>(ProjectsController)),
            ...(fetchMiddlewares<RequestHandler>(ProjectsController.prototype.drawWindowImage)),

            async function ProjectsController_drawWindowImage(request: ExRequest, response: ExResponse, next: any) {

            // WARNING: This file was auto-generated with tsoa. Please do not modify it. Re-run tsoa to re-generate this file: https://github.com/lukeautry/tsoa

            let validatedArgs: any[] = [];
            try {
                validatedArgs = templateService.getValidatedArgs({ args: argsProjectsController_drawWindowImage, request, response });

                const controller = new ProjectsController();

              await templateService.apiHandler({
                methodName: 'drawWindowImage',
                controller,
                response,
                next,
                validatedArgs,
                successStatus: undefined,
              });
            } catch (err) {
                return next(err);
            }
        });
        // WARNING: This file was auto-generated with tsoa. Please do not modify it. Re-run tsoa to re-generate this file: https://github.com/lukeautry/tsoa

    // WARNING: This file was auto-generated with tsoa. Please do not modify it. Re-run tsoa to re-generate this file: https://github.com/lukeautry/tsoa


    // WARNING: This file was auto-generated with tsoa. Please do not modify it. Re-run tsoa to re-generate this file: https://github.com/lukeautry/tsoa
}

// WARNING: This file was auto-generated with tsoa. Please do not modify it. Re-run tsoa to re-generate this file: https://github.com/lukeautry/tsoa
