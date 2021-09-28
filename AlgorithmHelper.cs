using System;
using System.Linq;
using Robot.Common;
using System.Collections.Generic;

namespace VitaliiKindrat.RobotChallenge
{
    public class AlgorithmHelper
    {
        private String Author = "Vitalii Kindrat";

        public int countMyRobots(IList<Robot.Common.Robot> robots)
        {
            return robots.Count(x => x.OwnerName == Author);
        }
        public int FindDistance(Position a, Position b)
        {
            return (int)(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
        public int GetRobotsInArea(Position position, IList<Robot.Common.Robot> robots)
        {
            var cells = GetAdjacentPositions(position);
            int robotCount = 0;
            foreach (var cell in cells)
            {
                foreach (var robot in robots)
                {
                    if (robot.Position == cell)
                    {
                        ++robotCount;
                    }
                }
            }
            return robotCount;
        }

        public bool IsAnybodyHere(Position position, IList<Robot.Common.Robot> robots, int maxAllowance)
        {
            return GetRobotsInArea(position, robots) > maxAllowance;
        }

        public bool InArea(Position checkedPosition, Position currentPosition)
        {
            var cells = GetAdjacentPositions(checkedPosition);
            return cells.Count(pos => pos == currentPosition) == 1;
        }

        public bool isEnoughEnergy(Robot.Common.Robot robot, Position desiredPosition)
        {
            return FindDistance(robot.Position, desiredPosition) <= robot.Energy;
        }

        public List<Position> GetAdjacentPositions(Position cell)
        {
            var resultCells = new List<Position>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    resultCells.Add(new Position(cell.X + i, cell.Y + j));
                }
            }

            return resultCells;
        }

        public Position FindNearestFreeStation(Robot.Common.Robot movingRobot, Map map, IList<Robot.Common.Robot> robots, int maxAllowance)
        {
            EnergyStation nearest = null;
            int minDistance = int.MaxValue;
            foreach (var station in map.Stations)
            {
                if (GetRobotsInArea(station.Position, robots) < maxAllowance + 1)
                {
                    int d = FindDistance(station.Position, movingRobot.Position);
                    if (d < minDistance)
                    {
                        minDistance = d;
                        nearest = station;
                    }
                }
            }

            return nearest == null ? null : nearest.Position;
        }

        public Position FindAveragePosition(Position currPosition, Position desiredPosition)
        {
            int x = (currPosition.X + desiredPosition.X) / 2;
            int y = (currPosition.Y + desiredPosition.Y) / 2;
            return new Position(x, y);
        }

        public Position FindTheBestPlaceToMove(Robot.Common.Robot robot, Map map, IList<Robot.Common.Robot> robots, int maxAllowance)
        {
            Position position = robot.Position;
            var stations = map.Stations.Where(x => isEnoughEnergy(robot, x.Position) && GetRobotsInArea(x.Position, robots) < maxAllowance).OrderBy(x => FindDistance(robot.Position, x.Position)).ToList();
            if (stations.Count == 0)
            {
                return null;
            }

            var cells = GetAdjacentPositions(stations[0].Position);
            Position bestCell = null;
            var minDistance = int.MaxValue;
            foreach (var x in cells)
            {
                if (!IsMyRobotOnStation(x, robots) && FindDistance(robot.Position, x) < minDistance)
                {
                    bestCell = x;
                    minDistance = FindDistance(robot.Position, x);
                }
            }

            return bestCell;
        }

        public bool IsMyRobotOnStation(Position cell, IEnumerable<Robot.Common.Robot> robots)
        {
            foreach (Robot.Common.Robot robot in robots)
            {
                if (robot.Position == cell)
                {
                    return robot.OwnerName == Author ? true : false;
                }
            }
            return false;
        }
    }
}
