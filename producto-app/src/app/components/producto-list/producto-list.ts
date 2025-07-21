import { Component, OnInit } from '@angular/core';
import { Producto } from '../../../app/models/producto';
import { ProductoService } from '../../../app/services/service';

@Component({
  selector: 'app-producto-list',
  standalone: true,
  imports: [],
  templateUrl: './producto-list.html',
  styleUrls: ['./producto-list.css'] 
})

export class ProductoListComponent {
  productos: Producto[] = [];
  productoEdit: Producto | null = null;

  constructor(private productoService: ProductoService) {}

  ngOnInit(): void {
    this.cargarProductos();
  }

  cargarProductos() {
    this.productoService.getProductos().subscribe(data => this.productos = data);
  }

  guardarProducto(producto: Producto) {
    if (producto.id === 0) {
      this.productoService.addProducto(producto).subscribe(() => this.cargarProductos());
    } else {
      this.productoService.updateProducto(producto).subscribe(() => this.cargarProductos());
    }
  }

  editarProducto(prod: Producto) {
    this.productoEdit = { ...prod };
  }

  eliminarProducto(id: number) {
    this.productoService.deleteProducto(id).subscribe(() => this.cargarProductos());
  }
}
  