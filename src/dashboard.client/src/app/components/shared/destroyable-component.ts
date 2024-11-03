import { Directive, OnDestroy } from "@angular/core";
import { Subject } from "rxjs";


@Directive()
export abstract class DestroyableComponent implements OnDestroy {
  protected readonly destroyed$: Subject<boolean> = new Subject<boolean>();

  ngOnDestroy(): void {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }

}
