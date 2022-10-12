using consoleProj;
using Microsoft.Windows.Themes;
using model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace Visualisation_Applications
{
    static class Visualisation
    {
        static Vector2 _racePos;


        static string straight_horizontal = ".\\Sprites\\horizontal.png";
        static string straight_vertical = ".\\Sprites\\vertical.png";
        static string Start_horizontal = ".\\Sprites\\horizontal finish.png";
        static string Start_vertical = ".\\Sprites\\verticalFinished.png";
        static string turnDown_Left = ".\\Sprites\\leftBottom.png";
        static string turnDown_Right = ".\\Sprites\\rightBottom.png";
        static string turnUp_Left = ".\\Sprites\\lefttTop.png";
        static string turnUp_Right = ".\\Sprites\\rightTop.png";




        public static BitmapSource DrawTrack(model.Track track)
        {


            if (track != null)
            {





                //initiolize variables
                Vector2 position = new Vector2();
                Vector2 direction = track.startDirection;
                LinkedList<Section> sections = track.Sections;
                int MinX = 0;
                int MinY = 0;
                int MaxX = 0;
                int MaxY = 0;
                Bitmap image = ImageProcessor.GenerateBitmap(MaxX - MinX, MaxY - MinY);
                Graphics G = Graphics.FromImage(image);
                for (int n = 0; n < 2; n++)
                {
                    if (_racePos != new Vector2(0, 0))
                    {
                        n++;
                        position = (Vector2)_racePos;
                    }

                    position = new Vector2(-MinX, -MinY);
                    direction = track.startDirection;
                    if (n == 1)
                    {
                        image = ImageProcessor.GenerateBitmap(MaxX - MinX, MaxY - MinY);
                        G = Graphics.FromImage(image);
                    }
                    for (int i = 0; i < sections.Count; i++)
                    {

                        SectionTypes curSection = sections.ElementAt(i).SectionType;



                        string imgPath = selectSectionSprite(ref direction, curSection);

                        if (n == 1)
                        {
                            drawSections(G, position, imgPath);
                        }






                        position += direction;
                        MinX = (int)(MinX < position.X ? MinX : position.X);
                        MinY = (int)(MinY < position.Y ? MinY : position.Y);

                        MaxX = (int)(MaxX > position.X ? MaxX : position.X);
                        MaxY = (int)(MaxY > position.Y ? MaxY : position.Y);

                    }


                }




            }
            return null;

        }
        public static void drawSections(Graphics G, Vector2 Pos, string section)
        {
            Image image = ImageProcessor.GetBitmap(section);
            G.DrawImage(image, new Point((int)Pos.X, (int)Pos.Y));
        }
        private static String selectSectionSprite(ref Vector2 direction, SectionTypes curSection)
        {
            string sectionToDraw = straight_horizontal;
            switch (curSection)
            {


                case SectionTypes.StartGrid:
                case SectionTypes.Finish:
                    {
                        if (Math.Abs(direction.Y) == 1)
                        {
                            sectionToDraw = Start_vertical;

                            break;
                        }
                        if (Math.Abs(direction.X) == 1)
                        {
                            sectionToDraw = Start_horizontal;

                            break;
                        }
                        break;
                    }
                case SectionTypes.Straight:
                    {
                        if (Math.Abs(direction.Y) == 1)
                        {
                            sectionToDraw = straight_vertical;

                            break;
                        }
                        if (Math.Abs(direction.X) == 1)
                        {
                            sectionToDraw = straight_horizontal;

                            break;
                        }
                        break;
                    }
                case SectionTypes.RightCornor:
                    {
                        if (direction.X == 1)
                        {
                            sectionToDraw = turnDown_Left;
                            direction = new Vector2(0, 1);
                            break;
                        }

                        if (direction.X == -1)
                        {
                            sectionToDraw = turnUp_Right;
                            direction = new Vector2(0, -1);
                            break;
                        }
                        if (direction.Y == -1)
                        {
                            sectionToDraw = turnDown_Right;
                            direction = new Vector2(1, 0);
                            break;
                        }

                        if (direction.Y == 1)
                        {
                            sectionToDraw = turnUp_Left;
                            direction = new Vector2(-1, 0);
                            break;
                        }
                        break;
                    }
                case SectionTypes.LeftCornor:
                    {
                        if (direction.X == 1)
                        {
                            sectionToDraw = turnUp_Left;
                            direction = new Vector2(0, -1);
                            break;
                        }

                        if (direction.X == -1)
                        {
                            sectionToDraw = turnDown_Right;
                            direction = new Vector2(0, 1);
                            break;
                        }
                        if (direction.Y == -1)
                        {
                            sectionToDraw = turnDown_Left;
                            direction = new Vector2(-1, 0);
                            break;
                        }

                        if (direction.Y == 1)
                        {
                            sectionToDraw = turnUp_Right;
                            direction = new Vector2(1, 0);
                            break;
                        }
                        break;
                    }

            }
            return sectionToDraw;
        }
    }
}
