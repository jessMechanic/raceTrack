using consoleProj;
using controller;
using Microsoft.Windows.Themes;
using model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using ViewButBetter;

namespace Visualisation_Applications
{
    static class Visualisation
    {
        static int timer = 0;

        #region graphics
        const string straight_horizontal = ".\\Sprites\\horizontal.png";
        const string straight_vertical = ".\\Sprites\\vertical.png";
        const string Start_horizontal = ".\\Sprites\\horizontal finish.png";
        const string Start_vertical = ".\\Sprites\\verticalFinished.png";
        const string turnDown_Left = ".\\Sprites\\leftBottom.png";
        const string turnDown_Right = ".\\Sprites\\rightBottom.png";
        const string turnUp_Left = ".\\Sprites\\lefttTop.png";
        const string turnUp_Right = ".\\Sprites\\rightTop.png";
        #endregion graphics



        static Dictionary<Section, TileData> GridData;
        static Vector2 GridSize;
        static Vector2 GridOffset;




        public static BitmapSource DrawTrack(model.Track track)
        {
            LinkedList<Section> sections = track.Sections;
            Bitmap image = ImageProcessor.GenerateBitmap((int)GridSize.X * 32, (int)GridSize.Y * 32);
            Graphics G = Graphics.FromImage(image);

            for (int i = sections.Count; i-- > 0;)
            {
                Section curSection = sections.ElementAt(i);
                SectionData SD = Data.CurrentRace.GetSectionData(curSection);
                TileData curTile = GridData[curSection];


                DrawSections(G, curTile.position - GridOffset, curTile.sprite);

                if (SD.Left != null)
                {
                    Image img = ImageProcessor.GetBitmap(".\\Sprites\\duckHor.png");
                    G.DrawImage(img, new Point((int)curTile.InnerAbsolute.X, (int)curTile.InnerAbsolute.Y));
                }
                if (SD.Right != null)
                {
                    Image img = ImageProcessor.GetBitmap(".\\Sprites\\duckHor.png");
                    G.DrawImage(img, new Point((int)curTile.OuterAbsolute.X, (int)curTile.OuterAbsolute.Y));
                   /* if (i > 10)
                    {
                        int c = 0;
                    }*/
                }

            }
            timer++;
            return ImageProcessor.CreateBitmapSourceFromGdiBitmap(image);
        }

        public static void PreCalculateGrid(Race race)
        {

            model.Track track = race.Track;
            if (track != null)
            {
                GridData = new Dictionary<Section, TileData>();
                Vector2 position = new();
                Vector2 direction = track.startDirection;
                LinkedList<Section> sections = track.Sections;
                int MinX = 0;
                int MinY = 0;
                int MaxX = 0;
                int MaxY = 0;






                for (int i = 0; i < sections.Count; i++)
                {
                    Section curSection = sections.ElementAt(i);
                    SectionTypes curSectionType = curSection.SectionType;
                    string spritePath = SelectSectionSprite(ref direction, curSectionType);

                    Vector2[] sides = CalcPosSides(curSectionType, direction);
                    GridData.Add(curSection, new TileData(spritePath, position, sides[0], sides[1]));

                    position += direction;
                    MinX = (int)(MinX < position.X ? MinX : position.X);
                    MinY = (int)(MinY < position.Y ? MinY : position.Y);

                    MaxX = (int)(MaxX > position.X ? MaxX : position.X);
                    MaxY = (int)(MaxY > position.Y ? MaxY : position.Y);

                }

                GridSize = new Vector2((MaxX - MinX + 1), (MaxY - MinY + 1));
                GridOffset = new Vector2(MinX, MinY);
            }



        }



        public static bool DrawSections(Graphics G, Vector2 Pos, string section)
        {
            Image image = ImageProcessor.GetBitmap(section);
            G.DrawImage(image, new Point((int)Pos.X * 32, (int)Pos.Y * 32));
            return true;
        }
        private static String SelectSectionSprite(ref Vector2 direction, SectionTypes curSection)
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
        private static Vector2[] CalcPosSides(SectionTypes sectionTypes, Vector2 direction)
        {
            Vector2[] sides = new Vector2[2];

            switch (sectionTypes)
            {


                case SectionTypes.StartGrid:
                case SectionTypes.Finish:
                case SectionTypes.Straight:

                    {
                        if (Math.Abs(direction.Y) == 1)
                        {
                            // Start_vertical;
                            sides[0] = new Vector2(16, 18);
                            sides[1] = new Vector2(8, 6);

                            break;
                        }
                        if (Math.Abs(direction.X) == 1)
                        {
                            // Start_horizontal;
                            sides[0] = new Vector2(1, 4);
                            sides[1] = new Vector2(22, 1);
                            break;
                        }
                        break;
                    }
                case SectionTypes.LeftCornor:
                case SectionTypes.RightCornor:
                    {
                        if (direction.X == 1)
                        {
                            //turnDown_Left;
                            sides[0] = new Vector2(16, 20);
                            sides[1] = new Vector2(3, 1);
                            break;
                        }

                        if (direction.X == -1)
                        {
                            //turnUp_Right;
                            sides[0] = new Vector2(23, 4);
                            sides[1] = new Vector2(8, 1);
                            break;
                        }
                        if (direction.Y == -1)
                        {
                            //turnDown_Right;
                            sides[0] = new Vector2(8, 20);
                            sides[1] = new Vector2(22, 1);
                            break;
                        }

                        if (direction.Y == 1)
                        {
                            //turnUp_Left;
                            sides[0] = new Vector2(0, 5);
                            sides[1] = new Vector2(17, 1);
                            break;
                        }
                        break;
                    }
                default:
                    {
                        sides[0] = new Vector2(10, 10);
                        sides[1] = new Vector2(20, 20);
                        break;
                    }

            }


            return sides;
        }
    }
}
