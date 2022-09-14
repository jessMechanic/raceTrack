using model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace consoleProj
{
    static class visualisation
    {
        
        #region graphics

        static string[] straight_horizontal = {"         ",
                                              "===========",
                                              "~~#~~~~~~~~",
                                              "~~~~~~#~~~~", 
                                              "===========",
                                              "         ",};

        static string[] straight_vertical =   {" ||~~~~||",
                                               " ||#~~~||",
                                               " ||~~~~||",
                                               " ||~~~~||",
                                               " ||~~~#||",
                                               " ||~~~~||",};


        static string[] Start_horizontal =   {"         ",
                                              "===========",
                                              "~~~~#█ ~~~~",
                                              "~#~~~ █~~~~",
                                              "===========",
                                              "         ",};
        static string[] Start_vertical =      {" ||~~~~||",
                                               " ||█ █ ||",
                                               " || █ █||",
                                               " ||#~~~||",
                                               " ||~~~~||",
                                               " ||~~~#||",};
        static string[] turnDown_Left = {"         ",
                                         "====--,",
                                          "~~~~~#\\\\",
                                          "~~~~~~~''",
                                          "=~#~~~~||",
                                          " \\\\~~~~||",
                                          " ||~~~~||" };


        static string[] turnDown_Right = {  "           ",
                                            "   _,--====",
                                            "  //#~~~~~~",
                                            " ''~~~~~~~~",
                                            " ||~~~~~~,=",
                                            " ||~~~#//  " };
        
        
        static string[] turnUp_Left = {
                                        " //#~~~||" ,
                                        "=`~~~~~|| " ,
                                        "~~~~~~~|| " ,
                                        "~~#~~~//  ",
                                        "====--`  " ,
                                        "           "};
        static string[] turnUp_Right = {
                                         " ||~~~~\\\\   ",
                                         " ||~~~~~#`==",
                                         " ||~#~~~~~~",
                                         "  \\\\~~~~~~~",
                                         "   ``--====",
                                        "           "};



       

        #endregion

        public static string[,,] TrackPlaces;
        public static void Initialize()
        {

        }

        public static void DrawTrack(Track track)
        {
            if (track != null)
            {

                

            

            //initiolize variables
            TrackPlaces = new string[8, 8,6];
            Vector2 position = track.startPosition;
            Vector2 direction = new Vector2(1, 0);
            LinkedList<Section> sections = track.Sections;



            Console.BackgroundColor = ConsoleColor.Green;
            Console.Clear();
            for (int i = 0; i < sections.Count; i++)
            {
                SectionTypes curSection = sections.ElementAt(i).SectionType;
                
                               
                string[] sectionToDraw = straight_horizontal;
                switch (curSection)
                {


                        case SectionTypes.StartGrid:
                        case    SectionTypes.Finish:
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
                            if (Math.Abs (direction.Y) == 1)
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
                            if(direction.X == 1) //√ 
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

                            if (direction.Y == 1)  //√ 
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
                






                for (int j = 0; j < sectionToDraw.Length; j++)
                {
                    //Console.SetCursorPosition((int)( i * 9), (int)( j));



                    for (int index = 0; index < sectionToDraw[j].Length; index++)
                    {
                        Console.SetCursorPosition((int)(position.X * 11 + index), (int)(position.Y * 6 + j));
                        char c = sectionToDraw[j][index];



                        if (c == '~' || c == '#')
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                             if(c == '~')
                            {
                                c = ' ';
                            }
                        }
                        else if (c != ' ')
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.White;

                        }
                        else {
                            Console.BackgroundColor = ConsoleColor.Green;
                        }
                            Console.Write(c);
                    }
                    

                }

                position += direction;







            }
            }
            Thread.Sleep(Timeout.Infinite);
        }

        

    }
    

}
