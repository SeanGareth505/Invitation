import { Component, ElementRef, Renderer2 } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent {
  value: string = '';
  private intervalId: any

  constructor(private _apiService: ApiService, private el: ElementRef, private renderer: Renderer2) {

  }

  ngOnInit() {
    // this.intervalId = setInterval(() => {
    //   this.createFallingFlower();
    // }, Math.random() * 2000 + 500); 
  }

  ngOnDestroy() {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  createFallingFlower() {
    const flower = this.renderer.createElement('div');
    this.renderer.addClass(flower, 'flower');
    this.renderer.setStyle(flower, 'background-image', 'url(../../assets/flower.png)');
    // Randomize the starting horizontal position
    const startPos = Math.random() * window.innerWidth - 50; // Ensuring it starts within the viewport
    this.renderer.setStyle(flower, 'left', `${startPos}px`);
  
    // Apply the animation with random duration to each flower for variety
    const animationDuration = `${Math.random() * 5 + 5}s`; // Duration between 5 and 10 seconds
    this.renderer.setStyle(flower, 'animation', `fall ${animationDuration} linear forwards`);
  
    const container = this.el.nativeElement.querySelector('.flower-field');
    this.renderer.appendChild(container, flower);
  
    // Remove the flower after it finishes falling to prevent DOM clutter
    setTimeout(() => {
      this.renderer.removeChild(container, flower);
    }, parseFloat(animationDuration) * 1000); // Convert duration to milliseconds
  }
  
  
  test() {
    this._apiService.testInvitation(this.value).subscribe({
      next: (response) => console.log('Test invitation sent successfully', response),
      error: (error) => console.error('Error sending test invitation', error)
    });
  }
}
