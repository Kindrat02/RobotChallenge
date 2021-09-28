using Robot.Common;
using System.Collections.Generic;
using VitaliiKindrat.RobotChallenge;

namespace VitaliiKindrat.ChallengeRobot
{
    public class Endspiel : IMyAlgorithm
    {
        private AlgorithmHelper helper = new AlgorithmHelper();
        private int MAX_ALLOWANCE = 2;

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            var myRobot = robots[robotToMoveIndex];
            Position position = helper.FindNearestFreeStation(myRobot, map, robots, MAX_ALLOWANCE);
            if (helper.InArea(position, myRobot.Position))
            {
                if (helper.GetRobotsInArea(position, robots) >= MAX_ALLOWANCE)
                {
                    position = helper.FindTheBestPlaceToMove(myRobot, map, robots, MAX_ALLOWANCE);
                    if (position != null)
                    {
                        return new MoveCommand() { NewPosition = position };
                    }         
                }
                else
                {
                    return new CollectEnergyCommand();
                }
            } 

            return new CollectEnergyCommand();
        }
    }
}
