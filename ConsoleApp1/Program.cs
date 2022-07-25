using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
namespace ConsoleApp1
{
    struct Spieler_Position
    {
        public int x, y;

    }
    struct Pfeil_Daten
    {
        public int x, y, zustand,richtung;
    }

    internal class Program
    {
        static Spieler_Position Player;
        static int[,] Feld;
        static Spieler_Position FeldGröße;
        static Spieler_Position[] Shield_Spawn = new Spieler_Position[3];
        static bool läuft;
        static Random Seiten_Auswahl = new Random();
        static bool attacke = false;
        static Pfeil_Daten[] Pfeile = new Pfeil_Daten[0];
        static int hp = 20;
        static int score = 0;
        static string NxtLine = "\n\n\n\n\n\n\n";
        static string Space = "                         ";
        static int pointer = 1;
        static bool In_Menu = true;
        static bool Will_Return = true;
        static void Main(string[] args)
        {
            Menu();
            

            
        }
        
        static void Menu()
        {
            

           
            
        
            
            string Ausgabe = "Welcome to my Arcade Game ! Use Arrow Keys.";
            

            for (int i = 0; i < Ausgabe.Length; i++)
            {
                Console.Write(Ausgabe[i]);
                Thread.Sleep(80);
            }

            Task MenuOptionen = Task.Factory.StartNew(() => Menu_Ausgabe());
            Task Auswahl = Task.Factory.StartNew(() => Menu_Movement());


            Task.WaitAll(Auswahl,MenuOptionen);


        }
        static void Menu_Ausgabe()
        {
            while (In_Menu )
            {
                if (Will_Return)
                {


                    Console.CursorVisible = false;
                    Console.SetCursorPosition(0, 0);
                    Console.Write(NxtLine + Space);

                    if (pointer == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    Console.Write("Play\n");

                    Console.ForegroundColor = ConsoleColor.White;

                    if (pointer == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    Console.Write(Space + "Information\n");

                    Console.ForegroundColor = ConsoleColor.White;

                    if (pointer == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    Console.Write(Space + "Exit\n");
                    Console.ForegroundColor = ConsoleColor.White;

                }

            }
        }

       
        static void Menu_Movement()
        {
            do
            {
                if (Will_Return)
                {
                    ConsoleKeyInfo r = Console.ReadKey(true);


                    switch (r.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (pointer > 1)
                            {
                                pointer--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (pointer < 3)
                            {
                                pointer++;
                            }
                            break;
                        case ConsoleKey.Enter:

                            switch (pointer)
                            {
                                case 1:
                                    In_Menu = false;
                                    config();

                                    Spiel_Starten();
                                    Console.Clear();
                                    break;

                                case 2:
                                    Will_Return = false;
                                    Info_Screen();
                                    Will_Return = true;

                                    break;
                                case 3:
                                    System.Environment.Exit(0);

                                    break;
                                default:
                                    break;
                            }

                            break;

                        default:
                            break;
                    }
                }


                    
            } while (In_Menu) ;
                
           
        }
        static void Info_Screen()
        {
            Console.Clear();
            string Txt = Space+"This Small Game was inspired from Undertale. \n" + Space +"Credits :\n\n"+Space+ "猫Vondalo猫#0002 \n"+Space+"Davs#1607";


            for (int i = 0; i < Txt.Length; i++)
            {
                Console.Write(Txt[i]);
                Thread.Sleep(30);
            }

            Console.WriteLine("\n\nPress any Key to return to Menu...");
            Console.ReadKey();
            Console.Clear();
        }

        static void config()
        {
            Console.CursorVisible = false;
            FeldGröße.x = 30; FeldGröße.y = 30;
            Feld = new int[30,30];
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            for (int i = 0; i < FeldGröße.x; i++)
            {
                Feld[0, i] = 1;
                Feld[FeldGröße.y-1, i] = 1;
            }
            for (int i = 0; i < FeldGröße.y; i++)
            {
                Feld[i, 0] = 1;
                Feld[i, FeldGröße.x-1] = 1;
            }
            

            Player.x = FeldGröße.x/2; Player.y = FeldGröße.y/2;

            läuft = true;                                  
        }



        static void Feld_Ausgabe()
        {
            while (läuft)
            {
                
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 0);

                Console.WriteLine("\n\n\n\n");
                for (int i = 0; i < FeldGröße.y; i++)
                {
                    Console.Write("                  ");
                    for (int j = 0; j < FeldGröße.x; j++)
                    {
                        if (Player.x == j && Player.y == i)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(" ♥");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (Feld[i,j] == 3)
                        {
                            Console.Write(" ↓");
                        }
                        else if (Feld[i, j] == 4)
                        {
                            Console.Write(" ↑");
                        }
                        else if (Feld[i, j] == 5)
                        {
                            Console.Write(" →");
                        }
                        else if (Feld[i, j] == 6)
                        {
                            Console.Write(" ←");
                        }
                        else if (Feld[i, j] == 1)
                        {
                            Console.Write("██");
                        }
                        else if (Feld[i, j] == 0)
                        {
                            Console.Write("  ");
                        }
                        else if (Feld[i,j] == 88)
                        {
                            Console.Write("--");
                        }
                        else if (Feld[i,j] == 99)
                        {
                            Console.Write(" |");
                        }
                    }

                    Console.WriteLine("                           ");
                }

                Console.Write("\n                             HP : ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(hp+ " " +"Score : "+ score+"         ");
                Console.ForegroundColor = ConsoleColor.White;
                if (hp <= 0)
                {
                    läuft = false;
                    
                    

                    Verloren();

                }

            }
        }

        static void Verloren()
        {
            Thread.Sleep(2000);
            Console.Clear();
            string text = "You Lost!"+  NxtLine+Space ;
            for (int i = 0; i < text.Length; i++)
            {

                Console.Write(text[i]);
                Thread.Sleep(65);
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Return to Menu");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.Clear();

            Will_Return = true;
            In_Menu = true;
            score = 0;
            hp = 20;
            Menu();
            
        }
        static void Spiel_Starten()
        {
            Task feldausgaben = Task.Factory.StartNew(() => Feld_Ausgabe());
            Task Bewegen = Task.Factory.StartNew(() => Movement());
            Task Pfeile_Spawner = Task.Factory.StartNew(() => Arrow_Shooter());
            Task Pfeile_Bewegen = Task.Factory.StartNew(() => Arrows_Bewegen());
            Task.WaitAll(feldausgaben,Bewegen, Pfeile_Spawner,Pfeile_Bewegen);
        }

        static void Movement()
        {
            while (läuft)
            {

                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (attacke)
                        {
                            Schild_Löschen();
                            Feld[Player.y - 2, Player.x] = 88;
                            Feld[Player.y - 2, Player.x -1] = 88;
                            Feld[Player.y - 2, Player.x +1] = 88;

                        }
                        else
                        {
                            if (Player.y - 1 >= 1)
                            {
                                Player.y--;
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (attacke)
                        {
                            Schild_Löschen();

                            Feld[Player.y + 2, Player.x] = 88;
                            Feld[Player.y + 2, Player.x - 1] = 88;
                            Feld[Player.y + 2, Player.x + 1] = 88;
                        }
                        else
                        {
                            if (Player.y + 1 < FeldGröße.y - 1)
                            {
                                Player.y++;
                            }
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (attacke)
                        {
                            Schild_Löschen();
                            Feld[Player.y, Player.x-2] = 99;
                            Feld[Player.y - 1, Player.x - 2] = 99;
                            Feld[Player.y + 1, Player.x - 2] = 99;
                        }
                        else
                        {
                            if (Player.x - 1 >= 1)
                            {
                                Player.x--;
                            }
                        }
                        break;
                    case ConsoleKey.RightArrow  :
                        if (attacke)
                        {
                            Schild_Löschen();
                            Feld[Player.y, Player.x + 2] = 99;
                            Feld[Player.y - 1, Player.x + 2] = 99;
                            Feld[Player.y + 1, Player.x + 2] = 99;
                        }
                        else
                        {
                            if (Player.x + 1 < FeldGröße.x - 1)
                            {
                                Player.x++;
                            }
                        }
                        break;
                    default:
                        break;  
                }
            }


            

        }
        static void Schild_Löschen()
        {
            for (int i = 1; i < FeldGröße.y-1; i++)
            {
                for (int j = 1; j < FeldGröße.x-1; j++)
                {
                    if (Feld[i,j] == 88 || Feld[i, j] == 99)
                    {
                        Feld[i, j] = 0;
                    }
                }
            }
        }
        static void Arrow_Shooter()
        {
            // 1 = Oben (3)| 2= Unten(4) | 3 = Links (5) | 4 = Rechts (6)| Pfeil = 3
            // Shield = 88
            while (läuft)
            {
                attacke = true;
                Thread.Sleep(990);
                int Seite = Seiten_Auswahl.Next(1, 5);
                Array.Resize(ref Pfeile, Pfeile.Length+1);
                Pfeile[Pfeile.Length - 1].richtung = Seite;
                switch (Seite)
                {
                    case 1:
                        Feld[1, Player.x] = 3;
                        Pfeile[Pfeile.Length - 1].x = Player.x; Pfeile[Pfeile.Length - 1].y = 1;
                        break;
                    case 2:
                        Feld[FeldGröße.y-2, Player.x] = 4;
                        Pfeile[Pfeile.Length - 1].x = Player.x; Pfeile[Pfeile.Length - 1].y = FeldGröße.y-2;
                        break;

                    case 3:
                        Feld[Player.y, 1] = 5 ;
                        Pfeile[Pfeile.Length - 1].x = 1; Pfeile[Pfeile.Length - 1].y = Player.y;
                        break;

                    case 4:
                        Feld[Player.y, FeldGröße.x-2] = 6;
                        Pfeile[Pfeile.Length - 1].x = FeldGröße.x-2; Pfeile[Pfeile.Length - 1].y = Player.y;
                        break;
                    default:
                        break;
                }




            }

        }

        static void Arrows_Bewegen()
        {
            
            int index = 0;
            int zustand = 0;
            while (läuft)
            {
                Thread.Sleep(200);

                for (int i = 0; i <= FeldGröße.y-2; i++)
                {

                    for (int j = 0; j <= FeldGröße.x-2; j++)
                    {
                        if (Feld[i, j] == 3)
                        {


                            for (int f = 0; f < Pfeile.Length; f++)
                            {
                                if (Pfeile[f].x == j && Pfeile[f].y == i)
                                {

                                    index = f;
                                    zustand = Pfeile[f].zustand;
                                }



                                // unten nach oben 
                                // (4)




                            }
                            if (Pfeile[index].zustand == 0)
                            {
                                if (Player.y - 1 == i)
                                {
                                    hp -= 2;
                                    Feld[i, j] = 0;
                                }
                                else if (Feld[i + 1, j] == 88)
                                {
                                    Feld[i, j] = 0;
                                    score += 15;
                                }
                                else
                                {
                                    Feld[i, j] = 0;
                                    Feld[i + 1, j] = 3;
                                    Pfeile[index].zustand = 1;
                                    Pfeile[index].y++;
                                }
                            }
                            else
                            {
                                Pfeile[index].zustand = 0;
                            }
                            











                        }

                    
                        // unten nach oben (4)

                        else if (Feld[i, j] == 5)
                        {

                            

                            for (int f = 0; f < Pfeile.Length; f++)
                            {
                                if (Pfeile[f].x == j && Pfeile[f].y == i)
                                {
                                    
                                    index = f;
                                    zustand = Pfeile[f].zustand;
                                }
                                


                                
                                // (5)




                            }
                            if (Pfeile[index].zustand == 0)
                            {
                                if (Player.x -1 == j)
                                {
                                    hp -= 2;
                                    Feld[i, j] = 0;
                                }
                                else if (Feld[i , j+1] == 99)
                                {
                                    Feld[i, j] = 0;
                                    score += 15;
                                }
                                else
                                {
                                    Feld[i, j] = 0;
                                    Feld[i, j+1] = 5;
                                    Pfeile[index].zustand = 1;
                                    Pfeile[index].x++;
                                }
                            }
                            else
                            {
                                Pfeile[index].zustand = 0;
                            }
                            











                        }
                    }
                }




                for (int i = FeldGröße.y-1; i >= Player.y ; i--)
                {

                    for (int j = FeldGröße.x-1; j >= Player.x; j--)
                    {
                        if (Feld[i, j] == 4)
                        {


                            for (int f = 0; f < Pfeile.Length; f++)
                            {
                                if (Pfeile[f].x == j && Pfeile[f].y == i)
                                {

                                    index = f;
                                    zustand = Pfeile[f].zustand;
                                }



                                // unten nach oben 
                                // (4)




                            }
                            if (Pfeile[index].zustand == 0)
                            {
                                if (Player.y + 1 == i)
                                {
                                    hp -= 2;
                                    Feld[i, j] = 0;
                                }
                                else if (Feld[i -1, j] == 88)
                                {
                                    Feld[i, j] = 0;
                                    score += 15;
                                }
                                else
                                {
                                    Feld[i, j] = 0;
                                    Feld[i -1, j] = 4;
                                    Pfeile[index].zustand = 1;
                                    Pfeile[index].y--;
                                }
                            }
                            else
                            {
                                Pfeile[index].zustand = 0;
                            }












                        }


                        // unten nach oben (4)

                        else if (Feld[i, j] == 6)
                        {



                            for (int f = 0; f < Pfeile.Length; f++)
                            {
                                if (Pfeile[f].x == j && Pfeile[f].y == i)
                                {

                                    index = f;
                                    zustand = Pfeile[f].zustand;
                                }




                                // (6)




                            }
                            if (Pfeile[index].zustand == 0)
                            {
                                if (Player.x +1 == j)
                                {
                                    hp -= 2;
                                    Feld[i, j] = 0;
                                }
                                else if (Feld[i, j -1] == 99)
                                {
                                    Feld[i, j] = 0;
                                    score += 15;
                                }
                                else
                                {
                                    Feld[i, j] = 0;
                                    Feld[i, j -1] = 6;
                                    Pfeile[index].zustand = 1;
                                    Pfeile[index].x--;
                                }
                            }
                            else
                            {
                                Pfeile[index].zustand = 0;
                            }












                        }
                    }
                }
            }
        }
    }
}
