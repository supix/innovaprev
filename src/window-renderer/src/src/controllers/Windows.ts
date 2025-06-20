import { Body, Controller, Get, Post, Query, Request, Route, Tags } from "tsoa";
import { Request as ExRequest } from "express";
import { Readable } from "stream";
import { DrawService } from '../services/draw.service';
import { WINDOW_MATERIAL_TYPES, WindowMaterialType } from '../interfaces/windows/windows.type';
import { GenericResponse } from '../interfaces/generic-response.interface';
import { WindowInputBatch } from '../interfaces/windows/window-input.interface';
import archiver from 'archiver';

@Route("windows")
@Tags("Windows")
export class ProjectsController extends Controller {

  private readonly drawService: DrawService;

  constructor() {
    super();
    this.drawService = new DrawService();
  }

  /**
   * @summary Returns all the codes of the window types managed by the application.
   * @returns {Promise<GenericResponse<WindowMaterialType[]>>} List of window type codes.
   */
  @Get('/drawableWindow')
  public async drawableWindow(): Promise<GenericResponse<WindowMaterialType[]>> {
    try {
      this.setStatus(200);
      return {
        data: [...WINDOW_MATERIAL_TYPES]
      };
    } catch (error) {
      this.setStatus(500);
      throw error;
    }
  }

  /**
   * Generates a raster image of a window based on height, width, and material type
   * and returns it as a PNG stream response.
   *
   * The image is scaled proportionally to match the input dimensions
   * and reflects the visual representation of the specified window material type:
   *
   * - 'CAS': Cassonetto
   * - 'F1A': Finestra 1 Anta
   * - 'F2A': Finestra 2 Ante
   * - 'FIX': Fisso centrale
   * - 'FLD': Fisso laterale destro
   * - 'FLS': Fisso laterale sinistro
   * - 'FRO': Frontalino
   * - 'PF1A': Portafinestra 1 Anta
   * - 'PF2A': Portafinestra 2 Ante
   * - 'PRT1A': Portoncino 1 Anta
   * - 'PRT2A': Portoncino 2 Ante
   * - 'SIL': Scorrevole in linea
   * - 'SLA': Sopraluce apribile
   * - 'SLF': Sopraluce fisso
   * - 'SRAF': Scorrevole ribalta con anta fissa
   * - 'SRLA': Scorrevole ribalta con laterale apribile
   * - 'VASC': Vasistas (apertura a cricchetto)
   * - 'VASM': Vasistas (apertura a motore)
   * - 'VAST': Vasistas (apertura a martellina)
   *
   * @summary Generate and download window raster image as PNG
   * @param height Height of the window in millimeters
   * @param width Width of the window in millimeters
   * @param materialType Material type of the window
   * @param wireCover Optional: include wire cover rendering
   * @param openingType Optional: direction of glass opening ('OT_DX' or 'OT_SX')
   * @param glassType Optional: glass type ('GT_TRASPARENTE' or 'GT_OPACO')
   * @param request Express request object used to stream file response
   * @returns PNG image buffer representing the requested window
   */
  @Get("/drawWindow")
  public async drawWindowImage(
    @Query() height: number,
    @Query() width: number,
    @Query() materialType: WindowMaterialType,
    @Request() request: ExRequest,
    @Query() wireCover?: boolean,
    @Query() openingType?: 'OT_DX' | 'OT_SX',
    @Query() glassType?: 'GT_TRASPARENTE' | 'GT_OPACO'
  ): Promise<void> {
    try {
      const fileName = `window_${materialType}_${width}x${height}.png`;

      const imageBuffer = this.drawService.drawWindow({
        height,
        width,
        materialType,
        wireCover,
        glassType,
        openingType,
      });

      const stream = Readable.from([imageBuffer]);

      request.res.setHeader("Content-Type", "image/png");
      request.res.setHeader("Content-Disposition", `inline; filename="${fileName}"`);
      stream.pipe(request.res);

      await new Promise<void>((resolve) => {
        stream.on("end", () => {
          request.res.end();
          resolve();
        });
      });

    } catch (err) {
      this.setStatus(500);
      throw err;
    }
  }


  /**
   * Generates and downloads a ZIP archive containing raster images for a set of windows.
   *
   * Each image is scaled and named based on the input parameters.
   * The response is streamed as a ZIP file with one PNG per window.
   *
   * @summary Generate and download multiple window previews as ZIP
   * @param inputs Array of window parameters
   * @param request Express request object for streaming the file
   * @returns ZIP archive containing one PNG image per input
   */
  @Post("/drawWindowsBatch")
  public async drawWindowsBatch(
    @Body() inputs: WindowInputBatch[],
    @Request() request: ExRequest,
  ): Promise<void> {
    try {

      if (!Array.isArray(inputs) || !inputs.length) {
        request.res.status(400).json({ error: 'Input array is empty or invalid.' });
        return;
      }

      const archive = archiver('zip', {zlib: {level: 9}});

      request.res.setHeader('Content-Type', 'application/zip');
      request.res.setHeader('Content-Disposition', 'attachment; filename="windows.zip"');

      archive.pipe(request.res);

      for (let i = 0; i < inputs.length; i++) {
        const input = inputs[i];
        const buffer = this.drawService.drawWindow(input);
        const name = `window_${input.position}.png`;
        archive.append(buffer, {name});
      }

      await archive.finalize();

      await new Promise<void>((resolve) => {
        archive.on("end", () => {
          request.res.end();
          resolve();
        });
      });

    } catch (error) {
      this.setStatus(500);
      throw error;
    }
  }


}
