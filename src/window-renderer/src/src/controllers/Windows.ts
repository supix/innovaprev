import { Controller, Get, Query, Request, Route, Tags } from "tsoa";
import { Request as ExRequest } from "express";
import { Readable } from "stream";
import { DrawService } from '../services/draw.service';

@Route("windows")
@Tags("Windows")
export class ProjectsController extends Controller {
    private readonly drawService: DrawService;

    constructor() {
        super();
        this.drawService = new DrawService();
    }

    /**
     * Generates a raster image of a window based on height, width, and type,
     * and returns it as a PNG stream response.
     *
     * The image is scaled proportionally to match the input dimensions,
     * and reflects the visual representation of the specified window type:
     * - 'double-leaf': two operable panes
     * - 'double-fixed': two fixed panes
     * - 'single-opening': single operable pane
     *
     * @summary Generate and download window raster image as PNG
     * @param height Height of the window in millimeters
     * @param width Width of the window in millimeters
     * @param type Type of window ('double-leaf', 'double-fixed', 'single-opening')
     * @param request Express request object used to stream file response
     * @returns PNG image buffer representing the requested window
     */
    @Get("/drawWindow")
    public async drawWindowImage(
      @Query() height: number,
      @Query() width: number,
      @Query() type: 'double-leaf' | 'double-fixed' | 'single-opening',
      @Request() request: ExRequest
    ): Promise<void> {
        try {
            const fileName = `window_${type}_${width}x${height}.png`;
            const imageBuffer = this.drawService.drawWindow({ height, width, type });

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



}
