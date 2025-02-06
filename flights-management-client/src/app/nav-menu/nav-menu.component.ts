import { Component } from '@angular/core';
import {RouterLink, RouterLinkActive} from "@angular/router";
import {NgClass} from "@angular/common";

@Component({
  selector: 'app-nav-menu',
  imports: [
    RouterLink,
    NgClass,
    RouterLinkActive
  ],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css',
  standalone: true
})
export class NavMenuComponent {
  isExpanded = false

  collapsed() {
    this.isExpanded = false
  }

  toggle() {
    this.isExpanded = !this.isExpanded
  }
}
