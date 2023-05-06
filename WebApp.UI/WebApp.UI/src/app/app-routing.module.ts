import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCourseComponent } from './components/add-course/add-course.component';
import { CoursesListComponent } from './components/courses/courses-list/courses-list.component';
import { EditCourseComponent } from './components/edit-course/edit-course.component';

const routes: Routes = [
{
  path: '',
  component: CoursesListComponent
},
{
  path: 'courses',
  component: CoursesListComponent
},
{
  path: 'courses/add',
  component: AddCourseComponent
},
{
  path: 'courses/edit/:id',
  component: EditCourseComponent
}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
