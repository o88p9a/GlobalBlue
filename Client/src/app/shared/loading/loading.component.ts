import {ChangeDetectionStrategy, Component, input, Input} from '@angular/core';
import {MatProgressBar} from '@angular/material/progress-bar';

@Component({
  selector: 'app-loading',
  imports: [
    MatProgressBar
  ],
  templateUrl: './loading.component.html',
  styleUrl: './loading.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoadingComponent {
  isLoading = input(false);
}
