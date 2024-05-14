import tkinter as tk
from tkinter import filedialog
import numpy as np
import cv2
from scipy.ndimage import gaussian_filter
import os
import json
from PIL import Image, ImageTk

class HeatmapGenerator:
    def __init__(self, root):
        self.root = root
        self.root.title("Heatmap Generator")

        self.click_coords = []
        self.image = None
        self.image_path = ""

        self.canvas = tk.Canvas(root)
        self.canvas.pack()

        self.button_frame = tk.Frame(root)
        self.button_frame.pack()

        self._create_buttons()
        self._bind_events()

    def _create_buttons(self):
        self.generate_button = tk.Button(self.button_frame, text="Generate Heatmap", command=self.on_generate_heatmap)
        self.generate_button.pack(side=tk.LEFT)

        self.save_coords_button = tk.Button(self.button_frame, text="Save Click Coordinates", command=self.save_click_coords)
        self.save_coords_button.pack(side=tk.LEFT)

        self.new_image_button = tk.Button(self.button_frame, text="Load New Image", command=self.load_new_image)
        self.new_image_button.pack(side=tk.LEFT)

    def _bind_events(self):
        self.canvas.bind("<Button-1>", self.on_click)

    def on_click(self, event):
        # Record click coordinates
        self.click_coords.append((event.x, event.y))
        # Display the click point on the image
        self.canvas.create_oval(event.x-2, event.y-2, event.x+2, event.y+2, fill='red', outline='red')

    def load_image(self):
        file_path = filedialog.askopenfilename(
            filetypes=[("Image files", "*.png;*.jpg;*.jpeg;*.bmp;*.tiff"), ("All files", "*.*")]
        )
        if not file_path:
            return None, file_path
        self.image_path = file_path
        return cv2.imread(file_path), file_path

    def create_heatmap(self, image, click_coords, gaussian_sigma=15):
        # Create an empty heatmap
        heatmap = np.zeros((image.shape[0], image.shape[1]), dtype=np.float32)

        # Convert click coordinates to heatmap
        for x, y in click_coords:
            heatmap[y, x] += 1

        # Apply Gaussian blur
        heatmap = gaussian_filter(heatmap, sigma=gaussian_sigma)

        # Normalize
        heatmap = (heatmap - np.min(heatmap)) / (np.max(heatmap) - np.min(heatmap))
        
        return heatmap

    def overlay_heatmap(self, image, heatmap, threshold=0.1):
        # Apply filter to ignore smaller data points
        heatmap[heatmap < threshold] = 0

        # Convert heatmap to color image
        heatmap_color = cv2.applyColorMap((heatmap * 255).astype(np.uint8), cv2.COLORMAP_JET)
        
        # Set regions without data in the heatmap to transparent
        heatmap_color[heatmap == 0] = 0

        # Overlay heatmap on the original image
        overlay = cv2.addWeighted(image, 1.0, heatmap_color, 0.6, 0)

        return overlay

    def save_image(self, image, save_path):
        cv2.imwrite(save_path, image)

    def save_click_coords(self):
        if self.image_path:
            base_name = os.path.basename(self.image_path)
            name, _ = os.path.splitext(base_name)
            save_path = os.path.join(os.path.dirname(self.image_path), f"{name}-gaze_coords.json")
            with open(save_path, 'w') as f:
                json.dump(self.click_coords, f)
            print(f"Click coordinates saved to {save_path}")

    def load_new_image(self):
        self.click_coords = []
        self.canvas.delete("all")
        self.image, file_path = self.load_image()
        if self.image is None:
            print("No image loaded.")
            return

        self.display_image(self.image)

    def display_image(self, image):
        # Convert OpenCV BGR image to RGB
        image_rgb = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
        self.image_pil = ImageTk.PhotoImage(image=Image.fromarray(image_rgb))

        self.canvas.config(width=image.shape[1], height=image.shape[0])
        self.canvas.create_image(0, 0, anchor=tk.NW, image=self.image_pil)

    def on_generate_heatmap(self):
        heatmap = self.create_heatmap(self.image, self.click_coords, gaussian_sigma=20)
        overlay = self.overlay_heatmap(self.image, heatmap, threshold=0.1)
        cv2.imshow("Heatmap Overlay", overlay)
        
        if self.image_path:
            base_name = os.path.basename(self.image_path)
            name, _ = os.path.splitext(base_name)
            save_path = os.path.join(os.path.dirname(self.image_path), f"{name}-gaze_hmap.{self.image_path.split('.')[-1]}")
            self.save_image(overlay, save_path)
            print(f"Heatmap saved to {save_path}")

def main():
    root = tk.Tk()
    app = HeatmapGenerator(root)
    root.mainloop()

if __name__ == "__main__":
    main()
