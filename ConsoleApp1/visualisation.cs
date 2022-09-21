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
using System.Threading.Tasks;

namespace consoleProj
{
    static class visualisation
    {
        private static Dictionary<Section, SectionData> _positions;

        private static Race _race;
        #region graphics

        static string[] straight_horizontal = {"         ",
                                              "===========",
                                              "~~1~~~~~~~~",
                                              "~~~~~~2~~~~", 
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
                                              "~~~~1█ ~~~~",
                                              "~2~~~ █~~~~",
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
                                            " ||~~~~~~,=",
                                            " ||~~~2//  " };
        
        
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
            _positions = race.GetPositions();
            _race = race;
        }

        public static void DrawTrack()
        {
            Track track = _race.Track;

            if (track != null)
            {

                

            

            //initiolize variables
            Vector2 position = track.startPosition;
            Vector2 direction = new Vector2(1, 0);
            LinkedList<Section> sections = track.Sections;



           // Console.BackgroundColor = ConsoleColor.Green;
            //Console.Clear();
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




                    SectionData sd = Data.CurrentRace.GetSectionData(sections.ElementAt(i));

                  //  Console.WriteLine(sd);
                  for (int j = 0; j < sectionToDraw.Length; j++)
                {
                        Console.SetCursorPosition((int)( i * 9), (int)( j));


                    for (int index = 0; index < sectionToDraw[j].Length; index++)
                        {
                            Console.SetCursorPosition((int)(position.X * 11 + index), (int)(position.Y * 6 + j));
                            char c = sectionToDraw[j][index];


                            switch (c)
                            {
                                case '~':
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    c = ' ';
                                    break;
                                case '1':
                                    Console.BackgroundColor = sd.Left != null ? sd.Left.getConsoleColor() : ConsoleColor.Blue;
                                    c = '>';
                                    break ;
                                case '2':
                                    Console.BackgroundColor = sd.Right != null? sd.Right.getConsoleColor() : ConsoleColor.Blue;
                                    c = '<';
                                    break;
                                case ' ':
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                       break;
                                default:
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
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
