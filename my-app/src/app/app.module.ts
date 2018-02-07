import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LumberCategoryComponent } from './lumber-category/lumber-category.component';
import { AppComponent } from './app.component';
import { CatalogComponent } from './catalog/catalog.component';


@NgModule({
  declarations: [
    AppComponent,
    LumberCategoryComponent,
    CatalogComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
