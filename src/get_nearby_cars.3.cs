using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;

public class AutoDriveSimulator : Script
{
    public AutoDriveSimulator()
    {
        // Set a timer to trigger every 100 milliseconds
        this.Tick += OnTick;
        this.Interval = 100;
    }

    private void OnTick(object sender, EventArgs e)
    {
        // Get the player's current position
        Vector3 playerPosition = Game.Player.Character.Position;

        // Get the vehicles around the player
        Vehicle[] nearbyVehicles = World.GetNearbyVehicles(playerPosition, 50f); // Search within a radius of 50 meters

        foreach (Vehicle vehicle in nearbyVehicles)
        {
            // Get the position of the vehicle
            Vector3 vehiclePosition = vehicle.Position;

            // Get the velocity of the vehicle
            Vector3 vehicleVelocity = vehicle.Velocity;
            float speed = vehicleVelocity.Length(); // Speed magnitude (scalar)

            // Print the retrieved information
            GTA.UI.Notification.Show("Vehicle position: " + vehiclePosition.ToString() + ", Speed: " + speed.ToString("0.00") + " m/s");

            // Get the model boundaries of the vehicle to calculate the Bounding Box
            Model vehicleModel = vehicle.Model;
            Vector3 minPoint, maxPoint;
            vehicleModel.GetDimensions(out minPoint, out maxPoint);

            // Calculate the positions of the four corners of the Bounding Box (rough calculation for ground vehicles)
            Vector3 frontLeft = vehiclePosition + new Vector3(minPoint.X, maxPoint.Y, minPoint.Z);
            Vector3 frontRight = vehiclePosition + new Vector3(maxPoint.X, maxPoint.Y, minPoint.Z);
            Vector3 rearLeft = vehiclePosition + new Vector3(minPoint.X, minPoint.Y, minPoint.Z);
            Vector3 rearRight = vehiclePosition + new Vector3(maxPoint.X, minPoint.Y, minPoint.Z);

            // Visualize the Bounding Box (for debugging purposes)
            World.DrawLine(frontLeft, frontRight, System.Drawing.Color.Red); // Front
            World.DrawLine(rearLeft, rearRight, System.Drawing.Color.Red); // Back
            World.DrawLine(frontLeft, rearLeft, System.Drawing.Color.Red); // Left side
            World.DrawLine(frontRight, rearRight, System.Drawing.Color.Red); // Right side
        }
    }
}
