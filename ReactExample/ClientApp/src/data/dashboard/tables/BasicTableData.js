// import media files
import Avatar1 from 'assets/images/avatar/avatar-1.jpg';
import Avatar2 from 'assets/images/avatar/avatar-2.jpg';
import Avatar3 from 'assets/images/avatar/avatar-3.jpg';
import Avatar4 from 'assets/images/avatar/avatar-4.jpg';
import Avatar5 from 'assets/images/avatar/avatar-5.jpg';
import Avatar6 from 'assets/images/avatar/avatar-6.jpg';
import Avatar7 from 'assets/images/avatar/avatar-7.jpg';
import Avatar8 from 'assets/images/avatar/avatar-8.jpg';

import DropboxLogo from 'assets/images/brand/dropbox-logo.svg';
import SlackLogo from 'assets/images/brand/slack-logo.svg';
import GithubLogo from 'assets/images/brand/github-logo.svg';
import ThreeDSmaxLogo from 'assets/images/brand/3dsmax-logo.svg';



const BasicTableData = [
    {
        id: 1,
        logo: DropboxLogo,
        project_name: 'Dropbox Design System',
        due_date: 'June 2',
        priority: 'Medium',
        badge: 'warning',
        members: [
            {
                id: 11,
                avatar: Avatar1
            },
            {
                id: 12,
                avatar: Avatar2
            },
            {
                id: 13,
                avatar: Avatar3
            },
            {
                id: 14,
                avatar: Avatar4
            },
            {
                id: 15,
                avatar: Avatar5
            },
            {
                id: 16,
                avatar: Avatar6
            },
            {
                id: 17,
                avatar: Avatar7
            },
            {
                id: 18,
                avatar: Avatar8
            }
        ]
    },
    {
        id: 2,
        logo: SlackLogo,
        project_name: 'Slack UI Design',
        due_date: 'June 12',
        priority: 'High',
        badge: 'danger',
        members: [
            {
                id: 21,
                avatar: Avatar1
            },
            {
                id: 22,
                avatar: Avatar2
            },
            {
                id: 23,
                avatar: Avatar3
            },
            {
                id: 24,
                avatar: Avatar4
            },
            {
                id: 25,
                avatar: Avatar5
            },
            {
                id: 26,
                avatar: Avatar6
            },
            {
                id: 27,
                avatar: Avatar7
            },
            {
                id: 28,
                avatar: Avatar8
            }
        ]
    },
    {
        id: 3,
        logo: GithubLogo,
        project_name: 'GitHub Satellite',
        due_date: 'Aug 14',
        priority: 'Low',
        badge: 'info',
        className: 'text-inverse',
        active: true,
        members: [
            {
                id: 31,
                avatar: Avatar1
            },
            {
                id: 32,
                avatar: Avatar2
            },
            {
                id: 33,
                avatar: Avatar3
            },
            {
                id: 34,
                avatar: Avatar4
            }
        ]
    },
    {
        id: 4,
        logo: ThreeDSmaxLogo,
        project_name: '3D Character Modelling',
        due_date: 'Sept 20',
        priority: 'Medium',
        badge: 'warning',
        members: [
            {
                id: 41,
                avatar: Avatar1
            },
            {
                id: 42,
                avatar: Avatar2
            },
            {
                id: 43,
                avatar: Avatar3
            },
            {
                id: 44,
                avatar: Avatar4
            },
            {
                id: 45,
                avatar: Avatar5
            },
            {
                id: 46,
                avatar: Avatar6
            },
            {
                id: 47,
                avatar: Avatar7
            },
            {
                id: 48,
                avatar: Avatar8
            }
        ]
    }
];

export default BasicTableData;
