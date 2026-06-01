using System;
using System.IO;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using Svg;
using System.Drawing;

namespace NetGestionConvalidacionBecasUni
{
    public static class Reportes
    {
        public static void GenerarPDF(string nombre, int cif, string correo, string tabla, string aprobado)
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("            GENERANDO REPORTE PDF REAL              ");
            Console.WriteLine("====================================================");

            // 1. Obtener y convertir el logo SVG a PNG
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            // Intentar resolver la ruta del directorio imgs en el proyecto
            string projectDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));
            string imgsDir = Path.Combine(projectDir, "imgs");
            if (!Directory.Exists(imgsDir))
            {
                imgsDir = Path.Combine(Directory.GetCurrentDirectory(), "imgs");
            }

            string svgPath = Path.Combine(imgsDir, "universidad_americana_2020.svg");
            string pngPath = null;

            if (File.Exists(svgPath))
            {
                pngPath = ConvertSvgToPng(svgPath);
            }

            try
            {
                // Crear documento PDF
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Reporte de Convalidación de Becas - UAM";
                
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Colores
                XColor uamColor = XColor.FromArgb(0, 156, 170); // #009caa de la clase s0 del SVG
                XColor darkTextColor = XColor.FromArgb(30, 30, 30);
                XColor lightGray = XColor.FromArgb(240, 240, 240);
                XColor borderGray = XColor.FromArgb(200, 200, 200);

                XPen borderPen = new XPen(borderGray, 1);
                XSolidBrush textBrush = new XSolidBrush(darkTextColor);
                XSolidBrush uamBrush = new XSolidBrush(uamColor);
                XSolidBrush headerBrush = new XSolidBrush(uamColor);
                XSolidBrush zebraBrush = new XSolidBrush(lightGray);

                // Fuentes
                XFont titleFont = new XFont("Arial", 16, XFontStyle.Bold);
                XFont subtitleFont = new XFont("Arial", 11, XFontStyle.Regular);
                XFont sectionFont = new XFont("Arial", 12, XFontStyle.Bold);
                XFont regularBoldFont = new XFont("Arial", 10, XFontStyle.Bold);
                XFont regularFont = new XFont("Arial", 10, XFontStyle.Regular);
                XFont smallFont = new XFont("Arial", 8, XFontStyle.Regular);

                // Dibujar Logo
                double yOffset = 40;
                if (!string.IsNullOrEmpty(pngPath) && File.Exists(pngPath))
                {
                    try
                    {
                        XImage logo = XImage.FromFile(pngPath);
                        // Escalar logo de UAM (original ~400x142) a ancho 120, alto 42
                        gfx.DrawImage(logo, 40, yOffset, 120, 42);
                    }
                    catch (Exception ex)
                    {
                        gfx.DrawString("UAM", titleFont, uamBrush, new XPoint(40, yOffset + 25));
                    }
                }
                else
                {
                    gfx.DrawString("UAM", titleFont, uamBrush, new XPoint(40, yOffset + 25));
                }

                // Títulos
                gfx.DrawString("SISTEMA DE CONVALIDACIÓN DE BECAS", titleFont, textBrush, new XPoint(180, yOffset + 20));
                gfx.DrawString("Ficha del Estudiante y Registro de Actividades", subtitleFont, uamBrush, new XPoint(180, yOffset + 38));

                yOffset += 60;

                // Línea separadora
                XPen uamPen = new XPen(uamColor, 2);
                gfx.DrawLine(uamPen, 40, yOffset, page.Width - 40, yOffset);

                yOffset += 20;

                // --- DATOS DEL ESTUDIANTE ---
                gfx.DrawString("DATOS DEL ESTUDIANTE", sectionFont, uamBrush, new XPoint(40, yOffset));
                yOffset += 15;

                double infoBoxHeight = 50;
                gfx.DrawRectangle(borderPen, 40, yOffset, page.Width - 80, infoBoxHeight);

                gfx.DrawString("Estudiante:", regularBoldFont, textBrush, new XPoint(50, yOffset + 20));
                gfx.DrawString(nombre, regularFont, textBrush, new XPoint(120, yOffset + 20));

                gfx.DrawString("CIF:", regularBoldFont, textBrush, new XPoint(50, yOffset + 38));
                gfx.DrawString(cif.ToString(), regularFont, textBrush, new XPoint(120, yOffset + 38));

                gfx.DrawString("Correo:", regularBoldFont, textBrush, new XPoint(300, yOffset + 20));
                gfx.DrawString(correo, regularFont, textBrush, new XPoint(350, yOffset + 20));

                yOffset += infoBoxHeight + 25;

                // --- DETALLE DE ACTIVIDADES ---
                gfx.DrawString("DETALLE DE ACTIVIDADES REGISTRADAS", sectionFont, uamBrush, new XPoint(40, yOffset));
                yOffset += 15;

                double tableX = 40;
                double colWidthAct = 130;
                double colWidthOrg = 120;
                double colWidthTipo = 120;
                double colWidthCant = 60;
                double colWidthFecha = 80;

                double currentX = tableX;
                gfx.DrawRectangle(headerBrush, currentX, yOffset, page.Width - 80, 20);

                XSolidBrush whiteBrush = new XSolidBrush(XColors.White);
                gfx.DrawString("Actividad", regularBoldFont, whiteBrush, new XPoint(currentX + 5, yOffset + 14));
                currentX += colWidthAct;
                gfx.DrawString("Organizador", regularBoldFont, whiteBrush, new XPoint(currentX + 5, yOffset + 14));
                currentX += colWidthOrg;
                gfx.DrawString("Tipo", regularBoldFont, whiteBrush, new XPoint(currentX + 5, yOffset + 14));
                currentX += colWidthTipo;
                gfx.DrawString("Cant.", regularBoldFont, whiteBrush, new XPoint(currentX + 5, yOffset + 14));
                currentX += colWidthCant;
                gfx.DrawString("Fecha", regularBoldFont, whiteBrush, new XPoint(currentX + 5, yOffset + 14));

                yOffset += 20;

                // Procesar actividades desde la cadena de texto
                string[] lines = tabla.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                bool zebra = false;

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string act = "", org = "", tipoVal = "", cant = "", fecha = "";
                    string[] parts = line.Split('|');
                    foreach (string part in parts)
                    {
                        string trimmed = part.Trim();
                        if (trimmed.StartsWith("Actividad:")) act = trimmed.Substring("Actividad:".Length).Trim();
                        else if (trimmed.StartsWith("Org:")) org = trimmed.Substring("Org:".Length).Trim();
                        else if (trimmed.StartsWith("Tipo:")) tipoVal = trimmed.Substring("Tipo:".Length).Trim();
                        else if (trimmed.StartsWith("Cant:")) cant = trimmed.Substring("Cant:".Length).Trim();
                        else if (trimmed.StartsWith("Fecha:")) fecha = trimmed.Substring("Fecha:".Length).Trim();
                    }

                    if (zebra)
                    {
                        gfx.DrawRectangle(zebraBrush, tableX, yOffset, page.Width - 80, 20);
                    }
                    gfx.DrawRectangle(borderPen, tableX, yOffset, page.Width - 80, 20);

                    currentX = tableX;
                    gfx.DrawString(act, regularFont, textBrush, new XPoint(currentX + 5, yOffset + 14));
                    currentX += colWidthAct;
                    gfx.DrawString(org, regularFont, textBrush, new XPoint(currentX + 5, yOffset + 14));
                    currentX += colWidthOrg;
                    gfx.DrawString(tipoVal, regularFont, textBrush, new XPoint(currentX + 5, yOffset + 14));
                    currentX += colWidthTipo;
                    gfx.DrawString(cant, regularFont, textBrush, new XPoint(currentX + 5, yOffset + 14));
                    currentX += colWidthCant;
                    gfx.DrawString(fecha, regularFont, textBrush, new XPoint(currentX + 5, yOffset + 14));

                    yOffset += 20;
                    zebra = !zebra;
                }

                yOffset += 25;

                // --- ESTADO DE LA BECA ---
                gfx.DrawString("ESTADO DE LA BECA", sectionFont, uamBrush, new XPoint(40, yOffset));
                yOffset += 15;

                gfx.DrawRectangle(borderPen, 40, yOffset, page.Width - 80, 40);
                
                XColor approvalColor = aprobado.StartsWith("SÍ") ? XColor.FromArgb(46, 117, 89) : XColor.FromArgb(163, 0, 0);
                XSolidBrush approvalBrush = new XSolidBrush(approvalColor);
                XFont approvalFont = new XFont("Arial", 12, XFontStyle.Bold);

                gfx.DrawString("Aprobación Vida Estudiantil:", regularBoldFont, textBrush, new XPoint(50, yOffset + 24));
                gfx.DrawString(aprobado, approvalFont, approvalBrush, new XPoint(220, yOffset + 24));

                yOffset += 65;

                // Pie de página
                gfx.DrawLine(borderPen, 40, page.Height - 60, page.Width - 40, page.Height - 60);
                gfx.DrawString("Universidad Americana - Dirección de Vida Estudiantil", smallFont, textBrush, new XPoint(40, page.Height - 48));
                gfx.DrawString("Este es un reporte oficial digital generado por el sistema de becas.", smallFont, textBrush, new XPoint(40, page.Height - 38));

                // Guardar PDF
                string outputFilename = $"Reporte_Convalidacion_{cif}.pdf";
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), outputFilename);
                document.Save(outputPath);

                Console.WriteLine("====================================================");
                Console.WriteLine($"PDF generado con éxito en: {outputPath}");
                Console.WriteLine("====================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"¡ERROR al generar el PDF! Detalle: {ex.Message}");
            }
        }

        private static string ConvertSvgToPng(string svgPath)
        {
            try
            {
                string pngPath = Path.ChangeExtension(svgPath, ".png");
                if (File.Exists(pngPath)) return pngPath;

                var svgDoc = Svg.SvgDocument.Open(svgPath);
                using (var bitmap = svgDoc.Draw())
                {
                    bitmap.Save(pngPath, System.Drawing.Imaging.ImageFormat.Png);
                }
                return pngPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Advertencia: No se pudo convertir el SVG a PNG. Detalle: {ex.Message}");
                return null;
            }
        }
    }
}
