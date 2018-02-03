import { Component, Input } from '@angular/core';
import { LumberCategory } from '../models/lumber-category';

@Component({
    selector: 'app-lumber-category',
    template: `
    <div style="margin: -20px 0 10px 0;">
    <div class="lumberCategoryView" style="margin-bottom: -2px;">
        <table>
            <tr>
                <td>
                    <h2>
                        <span id="lumberCategoryNameView">{{this.data.name}}</span>
                    </h2>
                </td>
                <td>
                    <div type="button" class="btn btn-primary lumberCategoryView"
                        (click)="showCategoryForm();" style="margin-top: 20px;">
                        <span class="glyphicon glyphicon-edit"></span> Edit
                    </div>
                </td>
            </tr>
        </table>
    </div>
    `
})
export class LumberCategoryComponent {
    @Input() data: LumberCategory;

    showCategoryForm() {
        console.log('showCategoryForm');
    }
}
