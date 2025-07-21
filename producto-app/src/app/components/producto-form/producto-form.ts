import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Producto } from '../../../app/models/producto';

@Component({
  selector: 'app-producto-form',
  imports: [],
  templateUrl: './producto-form.html',
  styleUrls: ['./producto-form.css']
})
export class ProductoFormComponent {
  @Input() producto: Producto = { id: 0, nombre: '', precio: 0 };
  @Output() onGuardar = new EventEmitter<Producto>();
  guardar() {
    if (this.producto.nombre.trim() === '') {
      alert("Debe ingresar el nombre del producto");
      return;
    }
    if (this.producto.precio <= 0){
      alert("Debe ingresar el precio del producto");
      return;
    }
    this.onGuardar.emit(this.producto);
    this.producto = { id: 0, nombre: '', precio: 0 }; // Limpiar formulario
  }
}