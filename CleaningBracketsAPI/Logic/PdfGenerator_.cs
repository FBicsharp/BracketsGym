using Microsoft.Extensions.Primitives;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CleaningBracketsAPI.Logic
{
	public class PdfGenerator_
	{
		private readonly ILogger<PdfGenerator_> logger;
		private readonly IHttpContextAccessor context;

		public PdfGenerator_(ILogger<PdfGenerator_> logger, IHttpContextAccessor context)
		{
			this.logger = logger;
			this.context = context;
		}

		/// <summary>
		/// Remove the external letter if matching
		/// </summary>
		/// <param name="strings"></param>
		/// <returns></returns>
		public byte[] GeneratePdf(List<string> inputString)
		{
			var endpoint = context.HttpContext.Request.Path;
			var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
			logger.LogInformation($"Request to endpoint {endpoint} from IP {ipAddress}");
			var pdfBytes = new byte[0];			
			var document = new PdfDocument();
			var page = document.AddPage();
			var gfx = XGraphics.FromPdfPage(page);			
			var font = new XFont("Arial", 11, XFontStyle.Regular);
			var format = new XStringFormat();
			var text = FormatString(inputString);


			var degre = 0;
			var stepx = 0;
			var stepy = 0;
			text.Split("\n").ToList().ForEach(x =>
				{
					gfx.RotateAtTransform(degre, new XPoint(300, 480));
					degre += 90;
					if (degre >= 360)
						degre = 0;

					stepx += 10;
					stepy += 10;

					gfx.DrawString(x, font, XBrushes.Black, new XRect(stepx, stepy, page.Width, page.Height), format);
				}
			);
				
			using (var stream = new MemoryStream())
			{
				document.Save(stream, false);
				pdfBytes = stream.ToArray();
			}
			return pdfBytes;
		}

		private string FormatString(List<string> inputString)
		{
			var longhestString = inputString.OrderByDescending(x => x.Count()).First().Length + 1;

			var contentMaps = new StringMapsGenerator(longhestString, longhestString);
			return contentMaps.Genratare(inputString);
		}
		
	}


	public class StringMapsGenerator
	{
		const int ROUND_SPACE = 3;//Considering that i colud have a roundend string , so I resevred the sapced for the rounded string
		const int START_END_ROUND_SPACE = 2;//Considering that i colud have a roundend string , so I resevred the sapced for the rounded string
		int x_Max { get; set; }
		int y_Max { get; set; }
		int Layer { get; set; }
		int x_currposition { get; set; }
		int y_currposition { get; set; }
		int x_startposition { get; set; }
		int y_startposition { get; set; }


		public char[,] Maps { get; set; }

		public StringMapsGenerator(int widthMax_, int heightMax_)
		{
			x_Max = widthMax_;
			y_Max = heightMax_;
			Maps = new char[x_Max, y_Max];
			for (int y = 0; y < y_Max; y++)
			{
				for (int x = 0; x < x_Max; x++)
				{
					Maps[y, x] = ' ';
				}
			}
		}

		public void MapContent(string s1, FaceSide Faceside)
		{
			if (Layer > 0 && Layer % 4 == 0)
			{
				x_currposition++;
				y_currposition++;
			}
			for (int i = 0; i < s1.Length; i++)
			{
				Maps[y_currposition, x_currposition] = s1[i];
				switch (Faceside)
				{
					case FaceSide.Top:
						x_currposition++;
						break;
					case FaceSide.Right:
						y_currposition++;
						break;
					case FaceSide.Bottom:
						x_currposition--;
						break;
					case FaceSide.Left:
						y_currposition--;
						break;
				}
			}
			Layer++;
		}

		private string GetContent()
		{
			var sb = new StringBuilder();

			for (int y = 0; y < y_Max; y++)
			{
				for (int x = 0; x < x_Max; x++)
				{
					sb.Append(Maps[y, x]);
                    Console.Write(Maps[y, x]);
                }
				sb.Append("\n");
				Console.WriteLine();
			}
			return sb.ToString();
		}


		public string Genratare(List<string> inputString)
		{
			var ordernput = inputString.OrderByDescending(x => x.Length).ToList();
			var faceSide = FaceSide.Top;
			for (int i = 0; i < ordernput.Count(); i++)
			{
				MapContent(ordernput[i], faceSide);
				faceSide++;
				if (faceSide > FaceSide.Left)
					faceSide = FaceSide.Top;
			}
			return GetContent();
		}


	}


	public enum FaceSide
	{
		Top,
		Right,
		Bottom,
		Left,
	}

}

