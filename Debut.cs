using Robot.Common;
using System.Collections.Generic;
using VitaliiKindrat.RobotChallenge;

namespace VitaliiKindrat.ChallengeRobot
{
    public class Debut : IMyAlgorithm
    {
        private const int MAX_ALLOWANCE = 1;
        private AlgorithmHelper helper = new AlgorithmHelper();

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            var myRobot = robots[robotToMoveIndex];
            Position position = helper.FindNearestFreeStation(myRobot, map, robots, MAX_ALLOWANCE);
            if (position != null && helper.InArea(position, myRobot.Position)) 
            {
                if (helper.IsAnybodyHere(position, robots, MAX_ALLOWANCE)) 
                {
                    position = helper.FindTheBestPlaceToMove(myRobot, map, robots, MAX_ALLOWANCE);
                    if (position != null)
                    {
                        return new MoveCommand() { NewPosition = position };
                    } 
                }
                else if (myRobot.Energy >= 250 && helper.countMyRobots(robots) < 100)
                {
                    return new CreateNewRobotCommand() { NewRobotEnergy = 100};
                }
                else
                {
                    return new CollectEnergyCommand();
                }
            }

            position = helper.FindTheBestPlaceToMove(myRobot, map, robots, MAX_ALLOWANCE);
            if (position != null && helper.isEnoughEnergy(myRobot, position))
            {
                return new MoveCommand() { NewPosition = position };
            }

            
            RobotCommand result;
            if (position != null && map.IsValid(position))
            {
                position = helper.FindAveragePosition(myRobot.Position, position);
                result = new MoveCommand() { NewPosition = position };
            }
            else
            {
                result = new CollectEnergyCommand();
            }
            return result;
        }
    }
}
