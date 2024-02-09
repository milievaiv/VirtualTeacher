// import node module libraries
import React, { Fragment, useMemo } from 'react';
import { Link } from 'react-router-dom';
import { Button, Image, Dropdown } from 'react-bootstrap';
import { XCircle, MoreVertical } from 'react-feather';

// import custom components
import DotBadge from 'components/elements/bootstrap/DotBadge';
import TanstackTable from 'components/elements/advance-table/TanstackTable';

const CoursesTable = ({ courses_data }) => {
	// The forwardRef is important!!
	// Dropdown needs access to the DOM node in order to position the Menu
	const CustomToggle = React.forwardRef(({ children, onClick }, ref) => (
		<Link
			to="#"
			ref={ref}
			onClick={(e) => {
				e.preventDefault();
				onClick(e);
			}}
			className="btn-icon btn btn-ghost btn-sm rounded-circle"
		>
			{children}
		</Link>
	));

	const ActionMenu = () => {
		return (
			<Dropdown>
				<Dropdown.Toggle as={CustomToggle}>
					<MoreVertical size="15px" className="text-secondary" />
				</Dropdown.Toggle>
				<Dropdown.Menu align="end">
					<Dropdown.Header>SETTINGS</Dropdown.Header>
					<Dropdown.Item eventKey="1">
						<XCircle size={14} className="me-1" /> Reject with Feedback
					</Dropdown.Item>
				</Dropdown.Menu>
			</Dropdown>
		);
	};

	const columns = useMemo(() => [
		{
			header: 'Courses',
			accessorKey: 'title',
			cell: ({ row, getValue }) => (
				<Link href="#" className="text-inherit">
					<div className="d-lg-flex align-items-center">
						<div>
							<Image
								src={row.original.image}
								alt=""
								className="img-4by3-lg rounded"
							/>
						</div>
						<div className="ms-lg-3 mt-2 mt-lg-0">
							<h4 className="mb-1 text-primary-hover">{getValue()}...</h4>
							<span className="text-inherit">
								{row.original.date_added}
							</span>
						</div>
					</div>
				</Link>
			),
		},
		{
			header: 'Instructor',
			accessorKey: 'instructor_name',
			cell: ({ row, getValue }) => (
				<div className="d-flex align-items-center">
					<Image
						src={row.original.instructor_image}
						alt=""
						className="rounded-circle avatar-xs me-2"
					/>
					<h5 className="mb-0">{getValue()}</h5>
				</div>
			),
		},
		{
			header: 'Status',
			accessorKey: 'status',
			cell: ({ getValue }) => (
				<Fragment>
					<DotBadge
						bg={
							getValue().toLowerCase() === 'pending'
								? 'warning'
								: getValue().toLowerCase() === 'live'
									? 'success'
									: ''
						}
					></DotBadge>
					{getValue().charAt(0).toUpperCase() + getValue().slice(1)}
				</Fragment>
			)
		},
		{
			header: 'Action',
			accessorKey: 'action',
			cell: ({ getValue }) => (
				getValue() === 1 ?
					<Button href="#" variant="secondary" className="btn-sm">
						Change Status
					</Button> :
					<Fragment>
						<Button
							href="#"
							variant="outline"
							className="btn-outline-secondary btn-sm"
						>
							Reject
						</Button>{' '}
						<Button href="#" variant="success" className="btn-sm">
							Approved
						</Button>
					</Fragment>

			)
		},
		{
			accessorKey: 'shortcutmenu',
			header: '',
			cell: () => {
				return <ActionMenu />;
			}
		}
	], []);

	const data = useMemo(() => courses_data, [])

	return (
		<TanstackTable
			data={data}
			columns={columns}
			filter={true}
			filterPlaceholder="Search Course"
			pagination={true} />
	);
};

export default CoursesTable;
