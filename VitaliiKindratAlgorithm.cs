using System.Collections.Generic;
using VitaliiKindrat.RobotChallenge;
using Robot.Common;

namespace VitaliiKindrat.ChallengeRobot
{
    public class VitaliiKindratAlgorithm : IRobotAlgorithm
    {
        private int roundNumber;
        private const int DEBUT_ROUND_LIMIT = 35;

        public VitaliiKindratAlgorithm()
        {
            Logger.OnLogRound += (sender, args) => ++roundNumber;
        }

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            IMyAlgorithm myAlgorithm;
            if (roundNumber <= DEBUT_ROUND_LIMIT)
            {
                myAlgorithm = new Debut();
            } 
            else
            {
                myAlgorithm = new Endspiel();
            }

            return myAlgorithm.DoStep(robots, robotToMoveIndex, map);
        }

        public string Author
        {
            get { return "Vitalii Kindrat"; }
        }

        public string Description
        {
            get { return "ZHYDACHIV"; }
        }

        //public void SetRound(int round)
        //{
        //    this.roundNumber = round;
        //}
    }
}
