using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GDAPS2Game
{
    class ScoreTracker
    {
        List<int> scores;
        public void WriteScore(int score)
        {
            try
            {
                scores = ReadScores();
                Stream str = File.Open("scores.dat", FileMode.Create);
                //Console.WriteLine("Writing the file");
                BinaryWriter output = new BinaryWriter(str);
                    int posOne = scores[0];
                    int posTwo = scores[1];
                    int posThree = scores[2];
                if (score > posThree)
                {
                    scores[1] = scores[2];
                    
                    scores[2] = score;
                }
                else
                {
                    if (score > posTwo)
                    {
                        scores[0] = scores[1];
                        scores[1] = score;
                    }
                    else
                    {
                        if (score > posOne)
                        {
                            scores[0] = score;
                        }
                    }
                }
                    output.Write(scores[2]);
                    output.Write(scores[1]);
                    output.Write(scores[0]);

                    output.Close();
                str.Close();
                //Console.WriteLine("Done writing");
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Error creating scores.dat: " + ioe.Message);
            }
        }



        public List<int>ReadScores()
        {
            scores = new List<int>();
            int scoreOne;
            int scoreTwo;
            int scoreThree;
            try
            {
                if (File.Exists("scores.dat"))
                {
                    BinaryReader input = new BinaryReader(File.OpenRead("scores.dat"));
                    //Console.WriteLine("Reading the file");
                    scoreOne = input.ReadInt32();

                    scoreTwo = input.ReadInt32();

                    scoreThree = input.ReadInt32();
                    scores.Add(scoreOne);
                    scores.Add(scoreTwo);
                    scores.Add(scoreThree);
                    input.Close();
                    //Console.WriteLine("Done Reading");
                    return scores;
                }
                else
                {
                     scoreOne = 0;
                     scoreTwo = 0;
                     scoreThree = 0;
                    scores.Add(scoreThree);
                    scores.Add(scoreTwo);
                    scores.Add(scoreOne);
                    //Console.WriteLine("File did not exist!");
                    return scores;
                }

            }
            catch(IOException ioe)
            {
                scores.Add(0);
                scores.Add(0);
                scores.Add(0);
           //     Console.WriteLine("Error reading scores.dat: " + ioe.Message);
                return scores;
                
            }
        }
    }
}
