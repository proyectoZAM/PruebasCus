import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

// import { App } from '../app/app';
import { ProductoListComponent } from '../app/components/producto-list/producto-list';
import { ProductoFormComponent } from '../app/components/producto-form/producto-form';


export class AppModule { 
  
}

export class App {
  protected readonly title = signal('producto-app');
}
