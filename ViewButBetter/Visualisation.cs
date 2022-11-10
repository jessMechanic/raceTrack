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
using Track = model.Track;
using Timer = System.Timers.Timer;

namespace Visualisation_Applications
{
    static class Visualisation
    {
        static int timer = 0;
        static int animationFrame = 0;
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
        static Track _track;



        public static BitmapSource DrawTrack(model.Track track)
        {
            animationFrame++;
            LinkedList<Section> sections = _track.Sections;
            Bitmap image = ImageProcessor.GenerateBitmap((int)GridSize.X * 32 , (int)GridSize.Y * 32);
            Graphics G = Graphics.FromImage(image);

            for (int i = sections.Count; i-- > 0;)
            {
                Section curSection = sections.ElementAt(i);
                SectionData SD = Data.CurrentRace.GetSectionData(curSection);
                if(GridData.ContainsKey(curSection)) { 
                TileData curTile = GridData[curSection];


                DrawSections(G, curTile.position - GridOffset, curTile.sprite);

                if (SD.Left != null)
                {
                    Image img = SD.Left.Equipment.isBroken ? ImageProcessor.GetBitmap($".\\Sprites\\animations\\broken{animationFrame   % 3 + 1}.png") : ImageProcessor.GetBitmap($".\\Sprites\\animations\\{SD.Left.Equipment.PartCostume}{animationFrame % 5 + 1}.png");
                        Image Coloredimg = ImageProcessor.ColorReplace((Bitmap)img, Color.FromArgb(128, 255, 0), SD.Left.TeamColor.toColor());
                    G.DrawImage(Coloredimg, new Point((int)(curTile.InnerAbsolute.X - GridOffset.X * 32), (int)(curTile.InnerAbsolute.Y - GridOffset.Y * 32)));
                }
                if (SD.Right != null)
                {
                    Image img = SD.Right.Equipment.isBroken ? ImageProcessor.GetBitmap($".\\Sprites\\animations\\broken{animationFrame % 3 + 1}.png") : ImageProcessor.GetBitmap($".\\Sprites\\animations\\{SD.Right.Equipment.PartCostume}{animationFrame % 5 + 1}.png");
                   Image  Coloredimg = ImageProcessor.ColorReplace((Bitmap)img, Color.FromArgb(128, 255, 0), SD.Right.TeamColor.toColor());
                    G.DrawImage(Coloredimg, new Point((int)(curTile.OuterAbsolute.X - GridOffset.X * 32), (int)(curTile.OuterAbsolute.Y - GridOffset.Y * 32)));
                    
                }

                }
            }
            timer++;
            return ImageProcessor.CreateBitmapSourceFromGdiBitmap(image);
        }

        public static void PreCalculateGrid(Race race)
        {
            ImageProcessor.ClearEmpty();

            _track = race.Track;
            if (_track != null)
            {
                GridData = new Dictionary<Section, TileData>();
                Vector2 position = new();
                Vector2 direction = _track.startDirection;
                LinkedList<Section> sections = _track.Sections;
                int MinX = 0;
                int MinY = 0;
                int MaxX = 0;
                int MaxY = 0;






                for (int i = 0; i < sections.Count; i++)
                {
                    Section curSection = sections.ElementAt(i);
                    SectionTypes curSectionType = curSection.SectionType;
                    string spritePath = SelectSectionSprite(ref direction, curSectionType);

                    Vector2[] sides = CalcPosSides(spritePath, direction);
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
        private static Vector2[] CalcPosSides(String sectionTypes, Vector2 direction)
        {
            Vector2[] sides = new Vector2[2];

            switch (sectionTypes)
            {


                case Start_horizontal:
                case straight_horizontal:
                {
                       
                            sides[0] = new Vector2(2, 9);
                            sides[1] = new Vector2(24, 13);

                            break;
                      
                    }
                case Start_vertical:
                case straight_vertical:
                    {
                        
                            sides[0] = new Vector2(10, 8);
                            sides[1] = new Vector2(17, 18);
                            break;
                       
                    }
                case turnDown_Left:
                    {
                        sides[0] = new Vector2(16, 10);
                        sides[1] = new Vector2(7, 28);
                        break;
                    }
              
                case turnUp_Right:
                    {
                        sides[0] = new Vector2(10, 2);
                        sides[1] = new Vector2(20, 13);
                        break;
                    } 
                case turnUp_Left:
                    {
                        sides[0] = new Vector2(2, 12);
                        sides[1] = new Vector2(15,8);
                        break;
                    }
                case turnDown_Right:
                    {
                        sides[0] = new Vector2(19, 28);
                        sides[1] = new Vector2(11,11);
                        break;
                    }
                default:
                    {
                        sides[0] = new Vector2(16, 10);
                        sides[1] = new Vector2(7, 28);
                        break;
                    }

            }


            return sides;
        }
    }
}
