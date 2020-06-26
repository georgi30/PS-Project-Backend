using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Persistence.Entities;
using PS_Project_Model.Utils.Interfaces;

namespace PS_Project_Model.Utils.Implementation
{
    public class PdfGeneratorUtils : IPdfGeneratorUtils
    {
        public string GeneratePdf(Recipe recipe)
        {
            var filepath = String.Format("/opt/ps/{0}.pdf", recipe.Name);
            FileStream fs = new FileStream(filepath, FileMode.Create);
            // Create an instance of the document class which represents the PDF document itself.  
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            // Create an instance to the PDF file by creating an instance of the PDF   
            // Writer class using the document and the file stream in the constructor.  
  
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            
            // Add meta information to the document  
            document.AddAuthor("E-Book Author");
            document.AddCreator("E-Book Creator");
            document.AddSubject("Recipe");
            document.AddTitle($"{recipe.Name}.pdf");
            
            // Open the document to enable you to write to the document  
            document.Open();
            // Add a simple and well known phrase to the document in a flow layout manner  
            document.Add(new Paragraph($"Directions: {recipe.Directions}"));
            document.Add(new Paragraph($"Preparation Time: {recipe.PreparationTime}"));
            document.Add(new Paragraph($"Cooking Time: {recipe.CookingTime}"));
            
            // Close the document  
            document.Close();
            // Close the writer instance  
            writer.Close();
            // Always close open filehandles explicity  
            fs.Close();

            return filepath;
        }
    }
}