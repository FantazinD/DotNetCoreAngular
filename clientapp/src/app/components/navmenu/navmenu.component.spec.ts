import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NavmenuComponent } from './navmenu.component';

describe('NavmenuComponent', () => {
  let fixture: ComponentFixture<NavmenuComponent>;
  let component: NavmenuComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavmenuComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(NavmenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
