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


        static string straight_horizontal = "/Sprite";
        static string straight_vertical;
        static string Start_horizontal;
        static string Start_vertical;
        static string turnDown_Left;
        static string turnDown_Right;
        static string turnUp_Left;
        static string turnUp_Right;




        public static BitmapSource DrawTrack(model.Track track)
        {


            if (track != null)
            {





                //initiolize variables
                Vector2 position = new Vector2();
                Vector2 direction = track.startDirection;
                LinkedList<Section> sections = track.Sections;
                Bitmap bitmap;

                int MinX = 0;
                int MinY = 0;
                int MaxX = 0;
                int MaxY = 0;


                for (int n = 0; n < 2; n++)
                {


                    if (_racePos != new Vector2(0,0))
                    {
                        n++;
                        position = (Vector2)_racePos;
                    }
                    if (n == 2)
                    {
                        bitmap = ImageProcessor.GenerateBitmap(MaxX - MinX, MaxY - MinY);
                    }
                    position = new Vector2(-MinX, -MinY);
                    direction = track.startDirection;

                    for (int i = 0; i < sections.Count; i++)
                    {

                        SectionTypes curSection = sections.ElementAt(i).SectionType;



                        string SectionToDraw = selectSectionSprite(ref direction, curSection);

                        if (n == 1)
                        {

                        }






                        position += direction;
                        MinX = (int)(MinX < position.X ? MinX : position.X);
                        MinY = (int)(MinY < position.Y ? MinY : position.Y);

                        MaxX = (int)(MaxX > position.X ? MaxX : position.X);
                        MaxY = (int)(MaxY > position.Y ? MaxY : position.Y);

                    }


                }
                return ImageProcessor.CreateBitmapSourceFromGdiBitmap(ImageProcessor.GenerateBitmap(MaxX - MinX, MaxY - MinY));
            }
            return null;

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
