using System.Text;

namespace CleaningBracketsAPI.Logic.Pdf
{
    public class StringMapsGenerator
    {        
        int x_Max { get; set; }
        int y_Max { get; set; }
        int Layer { get; set; }
        int x_currposition { get; set; }
        int y_currposition { get; set; }
        int lastLenght { get; set; }

        public CharMap[,] Maps { get; set; }

        public StringMapsGenerator(int widthMax_, int heightMax_)
        {
            x_Max = widthMax_;
            y_Max = heightMax_;
            Maps = new CharMap[x_Max, y_Max];
            for (int y = 0; y < y_Max; y++)
            {
                for (int x = 0; x < x_Max; x++)
                {
                    Maps[y, x] = new CharMap(' ',false,RoundedType.None, 0) ;
                }
            }
        }

        public void MapContent(string s1, FaceSide Faceside, bool isRounded )
        {
            if (Layer > 0 && Layer % 4 == 0)
            {
                x_currposition++;
                y_currposition++;
            }
            for (int i = 0; i < s1.Length; i++)
            {
                var orientationDegree = (short) (((int)Faceside)*90);				
                
                var roundedType = RoundedType.None;
                if (isRounded && i == 0)
					roundedType = RoundedType.Starting;
                else if (isRounded && i == s1.Length - 1)
                    roundedType = RoundedType.Ending;
				else if (isRounded)
                    roundedType = RoundedType.Central;
				else
                    roundedType = RoundedType.None;



				var c1 = new CharMap(s1[i], isRounded, roundedType, orientationDegree);

				Maps[y_currposition, x_currposition] = c1;
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

        public string GetStringConent()
        {
            var sb = new StringBuilder();

            for (int y = 0; y < y_Max; y++)
            {
                for (int x = 0; x < x_Max; x++)
                {
                    sb.Append(Maps[y, x].Symbol);
                    Console.Write(Maps[y, x].Symbol);
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }


        public CharMap[,] Generate(List<string> inputString)
        {
            var orderedInput = inputString.OrderByDescending(x => x.Length).ToList();
            var faceSide = FaceSide.Top;

            for (int i = 0; i < orderedInput.Count(); i++)
            {
                var currentStringLength = orderedInput[i].Length;                
				var isRounded =false;
                var numRows = orderedInput.Count();
				if (i > 0 && i+1 < numRows)
				{
					var prevStringLength = orderedInput[i - 1].Length;
					var nextStringLength = orderedInput[i + 1].Length;
					isRounded = currentStringLength == prevStringLength || currentStringLength == nextStringLength;
				}
				else if (i == 0 && i+1<numRows)
				{
					var nextStringLength = orderedInput[i + 1].Length;
					isRounded = currentStringLength == nextStringLength;
				}
				else if (i == numRows - 1 )
				{
					var prevStringLength = orderedInput[i - 1].Length;
					isRounded = currentStringLength == prevStringLength;
				}
				MapContent(orderedInput[i], faceSide, isRounded);
                faceSide++;
                if (faceSide > FaceSide.Left)
                    faceSide = FaceSide.Top;
            }
            return Maps;
        }


    }
    public record CharMap(char Symbol, bool IsRounded, RoundedType RoundedType, short OrientationDegree );
    public enum RoundedType
	{
        None,
        Starting,
		Central,
        Ending

    }

}

