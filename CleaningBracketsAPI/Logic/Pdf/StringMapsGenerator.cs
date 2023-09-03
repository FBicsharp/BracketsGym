using System.Text;

namespace CleaningBracketsAPI.Logic.Pdf
{
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
        List<string> inputString { get; set; }


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

        public string GetStringConent()
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
            }
            return sb.ToString();
        }


        public char[,] Generate(List<string> inputString)
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
            return Maps;
        }


    }

}

