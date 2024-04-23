using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;

public class AutoDriveSimulator : Script
{
    public AutoDriveSimulator()
    {
        // 设置一个定时器，每100毫秒触发一次
        this.Tick += OnTick;
        this.Interval = 100;
    }

    private void OnTick(object sender, EventArgs e)
    {
        // 获取玩家的当前位置
        Vector3 playerPosition = Game.Player.Character.Position;

        // 获取玩家周围的车辆
        Vehicle[] nearbyVehicles = World.GetNearbyVehicles(playerPosition, 50f); // 搜索半径50米内的车辆

        foreach (Vehicle vehicle in nearbyVehicles)
        {
            // 获取车辆的位置
            Vector3 vehiclePosition = vehicle.Position;

            // 获取车辆的速度
            Vector3 vehicleVelocity = vehicle.Velocity;
            float speed = vehicleVelocity.Length(); // 速度大小（标量）

            // 打印获取的信息
            GTA.UI.Notification.Show("车辆位置: " + vehiclePosition.ToString() + "，速度: " + speed.ToString("0.00") + " 米/秒");

            // 获取车辆的模型边界，用以计算Bounding Box
            Model vehicleModel = vehicle.Model;
            Vector3 minPoint, maxPoint;
            vehicleModel.GetDimensions(out minPoint, out maxPoint);

            // 计算Bounding Box的四个角的位置（仅对地面车辆粗略计算）
            Vector3 frontLeft = vehiclePosition + new Vector3(minPoint.X, maxPoint.Y, minPoint.Z);
            Vector3 frontRight = vehiclePosition + new Vector3(maxPoint.X, maxPoint.Y, minPoint.Z);
            Vector3 rearLeft = vehiclePosition + new Vector3(minPoint.X, minPoint.Y, minPoint.Z);
            Vector3 rearRight = vehiclePosition + new Vector3(maxPoint.X, minPoint.Y, minPoint.Z);

            // 可视化Bounding Box（调试用）
            World.DrawLine(frontLeft, frontRight, System.Drawing.Color.Red); // 前面
            World.DrawLine(rearLeft, rearRight, System.Drawing.Color.Red); // 后面
            World.DrawLine(frontLeft, rearLeft, System.Drawing.Color.Red); // 左侧
            World.DrawLine(frontRight, rearRight, System.Drawing.Color.Red); // 右侧
        }
    }
}
