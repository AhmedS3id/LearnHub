import { Link } from 'react-router-dom'
import type { CourseResponse } from '../../api/types'
import { Card } from '../ui/Card'
import { Badge } from '../ui/Badge'

export function CourseCard({ course }: { course: CourseResponse }) {
  return (
    <Link to={`/courses/${course.id}`} className="block h-full">
      <Card className="flex h-full flex-col transition-shadow hover:shadow-md">
        <div className="mb-2 flex items-start justify-between gap-2">
          <h3 className="line-clamp-2 text-base font-semibold text-slate-900">{course.title}</h3>
          <Badge tone="indigo">{course.categoryName}</Badge>
        </div>
        <p className="line-clamp-3 flex-1 text-sm text-slate-500">{course.description}</p>
        <div className="mt-4 flex items-center justify-between border-t border-slate-100 pt-3 text-sm">
          <span className="text-slate-500">By {course.instructorName}</span>
          <span className="font-semibold text-slate-900">${course.price.toFixed(2)}</span>
        </div>
      </Card>
    </Link>
  )
}
