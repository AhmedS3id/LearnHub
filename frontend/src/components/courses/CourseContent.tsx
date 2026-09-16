import { useState } from 'react'
import type { SectionWithLessonsResponse } from '../../api/types'
import { EmptyState } from '../ui/EmptyState'

function formatDuration(minutes: number): string {
  if (minutes < 60) return `${minutes} min`
  const hours = Math.floor(minutes / 60)
  const rest = minutes % 60
  return rest === 0 ? `${hours}h` : `${hours}h ${rest}m`
}

export function CourseContent({ sections }: { sections: SectionWithLessonsResponse[] }) {
  const [openSectionId, setOpenSectionId] = useState<number | null>(sections[0]?.id ?? null)

  if (sections.length === 0) {
    return <EmptyState title="No content yet" description="This course doesn't have any sections yet." />
  }

  return (
    <ul className="divide-y divide-slate-200 rounded-lg border border-slate-200">
      {sections.map((section) => {
        const isOpen = openSectionId === section.id
        return (
          <li key={section.id}>
            <button
              className="flex w-full items-center justify-between px-4 py-3 text-left hover:bg-slate-50"
              onClick={() => setOpenSectionId(isOpen ? null : section.id)}
              aria-expanded={isOpen}
            >
              <span className="text-sm font-medium text-slate-900">
                {section.order}. {section.title}
              </span>
              <span className="text-xs text-slate-500">{section.lessons.length} lessons</span>
            </button>
            {isOpen && (
              <ul className="divide-y divide-slate-100 bg-slate-50">
                {section.lessons.length === 0 ? (
                  <li className="px-6 py-3 text-sm text-slate-500">No lessons in this section yet.</li>
                ) : (
                  section.lessons.map((lesson) => (
                    <li key={lesson.id} className="flex items-center justify-between px-6 py-3 text-sm">
                      <span className="text-slate-700">
                        {lesson.order}. {lesson.title}
                      </span>
                      <span className="text-xs text-slate-500">{formatDuration(lesson.durationInMinutes)}</span>
                    </li>
                  ))
                )}
              </ul>
            )}
          </li>
        )
      })}
    </ul>
  )
}
