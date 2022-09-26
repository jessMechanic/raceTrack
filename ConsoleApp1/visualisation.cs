using controller;
using model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;

using System.Timers;

namespace consoleProj
{
    static class visualisation
    {

        private static Race _race;
        private static Vector2? _racePos;
        #region graphics

        static string[] straight_horizontal = {"         ",
                                              "===========",
                                              "~~1~~~~~~~~",
                                              "~~~~~~~~2~~",
                                              "===========",
                                              "         ",};

        static string[] straight_vertical =   {" ||~~~~||",
                                               " ||1~~~||",
                                               " ||~~~~||",
                                               " ||~~~~||",
                                               " ||~~~2||",
                                               " ||~~~~||",};


        static string[] Start_horizontal =   {"         ",
                                              "===========",
                                              "~1~~~~~~~█ ",
                                              "~~~~~~~2~ █",
                                              "===========",
                                              "         ",};
        static string[] Start_vertical =      {" ||~~~~||",
                                               " ||█ █ ||",
                                               " || █ █||",
                                               " ||1~~~||",
                                               " ||~~~~||",
                                               " ||~~~2||",};
        static string[] turnDown_Left = {"         ",
                                         "====--,",
                                          "~~~~~1\\\\",
                                          "~~~~~~~''",
                                          "=~2~~~~||",
                                          " \\\\~~~~||",
                                          " ||~~~~||" };


        static string[] turnDown_Right = {  "           ",
                                            "   _,--====",
                                            "  //1~~~~~~",
                                            " ''~~~~~~~~",
                                            " ||~~~~2~,=",
                                            " ||~~~~//  ",
                                            " ||~~~~||" };


        static string[] turnUp_Left = {
                                        " //1~~~||" ,
                                        "=`~~~~~|| " ,
                                        "~~~~~~~|| " ,
                                        "~~2~~~//  ",
                                        "====--`  " ,
                                        "           "};
        static string[] turnUp_Right = {
                                         " ||~~~~\\\\   ",
                                         " ||~~~~~1`==",
                                         " ||~2~~~~~~",
                                         "  \\\\~~~~~~~",
                                         "   ``--====",
                                        "           "};





        #endregion


        public static void Initialize(Race race)
        {
            _race = race;
            _racePos = null;
            _race.RaceTimer.Elapsed += OnTimedEvent;
        }
        public static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            DrawTrack();

        }
        public static void DrawTrack()
        {
            Track track = _race.Track;

            if (track != null)
            {





                //initiolize variables
                Vector2 position = new Vector2();
                Vector2 direction = _race.Track.startDirection;
                LinkedList <Section> sections = track.Sections;

                ConsoleColor themeColor = _race.Track.ThemeColor;
                ConsoleColor boundryColor = _race.Track.TrackBoundry;
                ConsoleColor trackColor = _race.Track.TrackColor;

                Console.BackgroundColor = themeColor;
                Console.Clear();
                float MinX = 0;
                float MinY = 0;

              

                for (int n = 0; n < 2; n++)
                {
                   if(_racePos != null)
                    {
                        n++;
                        position = (Vector2)_racePos;
                    }
                    
                        position = new Vector2(-MinX , -MinY );
                        direction = _race.Track.startDirection;

                    for (int i = 0; i < sections.Count; i++)
                    {

                        SectionTypes curSection = sections.ElementAt(i).SectionType;


                        string[] sectionToDraw = straight_horizontal;
                        visualisation.selectSectionSprite(ref direction, curSection, ref sectionToDraw);

                        if (n == 1)
                        {
                            drawSections(position, direction, sections, themeColor, boundryColor, trackColor, i, sectionToDraw);
                        }






                        position += direction;
                        MinX = MinX < position.X ? MinX : position.X;
                        MinY = MinY < position.Y ? MinY : position.Y;

                    }


                } 
            }

       
        }

        private static void drawSections(Vector2 position, Vector2 direction, LinkedList<Section> sections, ConsoleColor themeColor, ConsoleColor boundryColor, ConsoleColor trackColor, int i, string[] sectionToDraw)
        {
            SectionData sd = Data.CurrentRace.GetSectionData(sections.ElementAt(i));


            for (int j = 0; j < sectionToDraw.Length; j++)
            {



                for (int index = 0; index < sectionToDraw[j].Length; index++)
                {
                    Console.SetCursorPosition((int)(position.X * 11 + index), (int)(position.Y * 7 + j));
                    char c = sectionToDraw[j][index];

                    int face = (int)(direction.X + direction.Y);
                    switch (c)
                    {
                        case '~':
                            Console.BackgroundColor = trackColor;
                            c = ' ';
                            break;
                        case '1':
                            if (face == -1)
                            {
                                if (sd.Right != null)
                                {
                                    Console.BackgroundColor = sd.Right.TeamColor.toConsoleColor();
                                    c = '░';
                                    break;
                                }
                            }
                            else
                            {
                                if (sd.Left != null)
                                {
                                    Console.BackgroundColor = sd.Left.TeamColor.toConsoleColor();
                                    c = '░';
                                    break;
                                }
                            }
                            Console.BackgroundColor = trackColor;
                            c = ' ';
                            break;
                        case '2':
                            if (face == 1)
                            {
                                if (sd.Right != null)
                                {
                                    Console.BackgroundColor = sd.Right.TeamColor.toConsoleColor();
                                    c = '░';
                                    break;
                                }
                            }
                            else
                            {
                                if (sd.Left != null)
                                {
                                    Console.BackgroundColor = sd.Left.TeamColor.toConsoleColor();
                                    c = '░';
                                    break;
                                }
                            }

                            Console.BackgroundColor = trackColor;
                            c = ' ';
                            break;
                        case ' ':
                            Console.BackgroundColor = themeColor;
                            continue;

                        default:
                            Console.BackgroundColor = boundryColor;
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }


                    Console.Write(c);


                }


            }
        }

        private static void selectSectionSprite(ref Vector2 direction, SectionTypes curSection, ref string[] sectionToDraw)
        {
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
        }
    }


}
