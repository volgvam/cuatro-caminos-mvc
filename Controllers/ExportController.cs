using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.rtf.headerfooter;
using iTextSharp.text.xml;
using Color = iTextSharp.text.Color;
using Font = iTextSharp.text.Font;


namespace CuatroCaminosMvcApplication.Controllers
{
    [Authorize]
    public class ExportController : Controller
    {
        private static String fontsDirName = (AppDomain.CurrentDomain.BaseDirectory + "fonts");
        private static BaseFont baseFont = BaseFont.CreateFont(fontsDirName + "\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

        iTextSharp.text.Font fontLarge = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font fontSmail = new iTextSharp.text.Font(baseFont, 11, iTextSharp.text.Font.NORMAL, Color.BLACK);
        iTextSharp.text.Font fontSmailer = new iTextSharp.text.Font(baseFont, 9, iTextSharp.text.Font.NORMAL, Color.BLACK);


 //       iTextSharp.text.Font font, fontSmail, fontLarge, fontSmailer;
 //       private PdfContentByte pdfContent;
 //       private PdfTemplate pageNumberTemplate;
 //       private DateTime printTime;
        private PdfPCell cell;


        PdfPTable table = new PdfPTable(14);

        Document document = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 5);
        MemoryStream memoryStream = new MemoryStream();


        DataManager dataManager = new DataManager();


        public FileResult Index(int gruppa)
        {

//            gruppa = Request.QueryString["Group"]}

//            int gruppa = collection["gruppa"] != null ? Convert.ToInt32(collection["gruppa"]) : 0;

            ICollection<List_IdAndName> listIdAndNames = dataManager.GetExportСписокОплаты(gruppa);




            

            PdfWriter pdfWriter =  PdfWriter.GetInstance(document, memoryStream);
            PdfContentByte pdfContentByte = new PdfContentByte(pdfWriter);

//            PdfTextArray pdfTextArray = new PdfTextArray(String.Format("                 Список оплаты Клуба Танцев \"Cuatro Caminos\".       группа: {0}", dataManager.GetNameGroupFromId(gruppa)));

            pdfWriter.CloseStream = false;

            var welcomeParagraph = new Paragraph(String.Format("     Список оплаты Клуба Танцев \"Cuatro Caminos\".          {0}             месяц: _____________ {1}", dataManager.GetNameGroupFromId(gruppa), DateTime.Now.Year), fontLarge);

            HeaderFooter headerFooter = new HeaderFooter(
                    new Phrase("Всего: " + listIdAndNames.Count + " человек                                                  " + "описание группы: " + dataManager.GetDescGroupFromId(gruppa) + "                                                                   " + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "                ", fontSmailer), true);

            headerFooter.Border = iTextSharp.text.Rectangle.NO_BORDER;
            headerFooter.Alignment = 2;
            headerFooter.Top = 0;
            headerFooter.Bottom = 0;


            document.Footer = headerFooter;
            headerFooter.Top = 0;
            headerFooter.Bottom = 0;
//            headerFooter.Before = welcomeParagraph;   


            document.Open();


            document.AddTitle("Список оплаты Клуба Танцев \"Cuatro Caminos\"");
            document.AddAuthor("Вадим");
            
            document.Add(welcomeParagraph);


//            HeaderFooter headerFooter= new HeaderFooter();


//            table = new PdfPTable(14);

            table.TotalWidth = 830f;
            //fix the absolute width of the table
            table.LockedWidth = true;

            //relative col widths in proportions - 1/3 and 2/3
            float[] widths = new float[] { 3f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            //leave a gap before and after the table
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;


            int ii = 1;


//            PdfPCell cell_fio;

           

            foreach (var item in listIdAndNames)
            {


                if (ii == 1 || ii == 21 || ii == 42)
                {
                    ShapkaTable();
                }


                cell = new PdfPCell(new Phrase(String.Format(" {1}", ii, item.Names), fontSmail));

                cell.PaddingBottom = 6f;
                cell.PaddingTop = 6f;


                table.AddCell(cell);

                for (int i = 0; i < 13; i++)
                {
                 table.AddCell("");   
                }

                ii++;
            }

            if (listIdAndNames.Count<21)
            {
                for (int i = 0; i < 41 - listIdAndNames.Count; i++)
                {
                    cell = new PdfPCell(new Phrase(""));

                    cell.PaddingBottom = 19f;

                    table.AddCell(cell);

                    for (int a = 0; a < 13; a++)
                    {
                        table.AddCell("");
                    }

                }
                
            }

                FooterTable();
            

            document.Add(table);

            document.Close();

            byte[] bytes = new byte[memoryStream.Position];
            memoryStream.Position = 0;
            memoryStream.Read(bytes, 0, bytes.Length);


            return File(bytes, "application/pdf", "PhoneEport_.pdf");
        }

        private PdfPTable ShapkaTable()
        {


            cell = new PdfPCell(new Phrase("ФИО", fontLarge));
            //            cell.BackgroundColor = new Color(0, 150, 0);
            //            cell.BorderColor = new Color(255, 242, 0);
            //            cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            //            cell.BorderWidthBottom = 3f;
            //            cell.BorderWidthTop = 3f;
            cell.PaddingBottom = 10f;
            //            cell.PaddingLeft = 15f;
            cell.PaddingTop = 10f;


            cell.VerticalAlignment = 1;
            cell.HorizontalAlignment = 1;

//            PdfPTable table = new PdfPTable(14);

            table.AddCell(cell);

            table.AddCell(new Phrase(new Chunk("Перенос с прошлого месяца", fontSmailer)));

            for (int i = 1; i < 13; i++)
            {
                table.AddCell(new Phrase(new Chunk(String.Format("{0} занятие", i), fontSmailer)));
            }

//            headerFooter = new  RtfHeaderFooter(table);

            return table;
        }

        private PdfPTable FooterTable()
        {


            cell = new PdfPCell(new Phrase(new Chunk("Сумма за день", font)));
            cell.VerticalAlignment = 2;
            cell.HorizontalAlignment = 1;
            cell.Colspan = 2;
            cell.PaddingBottom = 15f;
            cell.PaddingTop = 15f;



            table.AddCell(cell);

            for (int i = 1; i < 13; i++)
            {

                table.AddCell(new Phrase(new Chunk(String.Format("{0} занятие", i), fontSmailer)));
            }




            cell = new PdfPCell(new Phrase(new Chunk("Количество людей", font)));
            cell.Colspan = 2;
            cell.PaddingBottom = 7f;
            cell.PaddingTop = 7f;
            cell.VerticalAlignment = 1;
            cell.HorizontalAlignment = 1;

            table.AddCell(cell);


            for (int i = 1; i < 13; i++)
            {
                table.AddCell("");
            }




            
            cell = new PdfPCell(new Phrase(new Chunk("Тренер", font)));
            cell.Colspan = 2;
            cell.PaddingBottom = 7f;
            cell.PaddingTop = 7f;
            cell.VerticalAlignment = 1;
            cell.HorizontalAlignment = 1;

            table.AddCell(cell);


            for (int i = 1; i < 13; i++)
            {
                table.AddCell("");
            }

            return table;
        }



    }
}
