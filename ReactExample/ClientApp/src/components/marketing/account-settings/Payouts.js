// import node module libraries
import React, { useState, useMemo } from 'react';
import {
	Card,
	Row,
	Col,
	Dropdown,
	Badge,
	Image,
	Alert,
	Form,
	Table,
	Button
} from 'react-bootstrap';
import { Link } from 'react-router-dom';
import {
	flexRender,
	getCoreRowModel,
	getFilteredRowModel,
	getPaginationRowModel,
	useReactTable,
} from '@tanstack/react-table';
import { Trash, Edit, MoreVertical } from 'react-feather';

// import media files
import PayPal from 'assets/images/brand/paypal-small.svg';
import Payoneer from 'assets/images/brand/payoneer.svg';

// import custom components
import Pagination from 'components/elements/advance-table/Pagination';
import ApexCharts from 'components/elements/charts/ApexCharts';
import { FormSelect } from 'components/elements/form-select/FormSelect';

// import utility file
import { numberWithCommas } from 'helper/utils';

// Import dashboard layout
import ProfileLayout from 'components/marketing/instructor/ProfileLayout';

// import data files
import { WithdrawHistoryData } from 'data/marketing/WithdrawHistoryData';
import { PayoutChartSeries, PayoutChartOptions } from 'data/charts/ChartData';

const Payouts = () => {
	const [filtering, setFiltering] = useState('');
	const [rowSelection, setRowSelection] = useState({});

	// The forwardRef is important!!
	// Dropdown needs access to the DOM node in order to position the Menu
	const CustomToggle = React.forwardRef(({ children, onClick }, ref) => (
		<Link
			to=""
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
					<Dropdown.Header>SETTINGS--</Dropdown.Header>
					<Dropdown.Item eventKey="1">
						{' '}
						<Edit size="15px" className="dropdown-item-icon" /> Edit
					</Dropdown.Item>
					<Dropdown.Item eventKey="2">
						{' '}
						<Trash size="15px" className="dropdown-item-icon" /> Remove
					</Dropdown.Item>
				</Dropdown.Menu>
			</Dropdown>
		);
	};
	const columns = useMemo(
		() => [
			{
				accessorKey: 'id',
				header: 'ID',
				cell: ({ getValue }) => {
					return '#' + getValue();
				}
			},
			{ accessorKey: 'method', header: 'Method' },
			{
				accessorKey: 'status',
				header: 'Status',
				cell: ({ getValue }) => {
					return (
						<Badge
							bg={`${getValue() === 'Pending'
								? 'warning'
								: getValue() === 'Paid'
									? 'success'
									: 'danger'
								} `}
						>
							{getValue()}
						</Badge>
					);
				}
			},
			{
				accessorKey: 'amount',
				header: 'Amount',
				cell: ({ getValue }) => {
					return '$' + numberWithCommas(getValue());
				}
			},
			{ accessorKey: 'date', header: 'Date' },
			{
				accessorKey: 'actionmenu',
				header: '',
				cell: () => {
					return <ActionMenu />;
				}
			}
		],
		[]
	);

	const data = useMemo(() => WithdrawHistoryData, []);

	const table = useReactTable({
		data,
		columns,
		getCoreRowModel: getCoreRowModel(),
		getPaginationRowModel: getPaginationRowModel(),
		getFilteredRowModel: getFilteredRowModel(),
		state: {
			globalFilter: filtering,
			rowSelection
		},
		enableRowSelection: true,
		onRowSelectionChange: setRowSelection,
		onGlobalFilterChange: setFiltering,
		debugTable: false,
	})

	const AlertDismissible = () => {
		const [show, setShow] = useState(true);
		if (show) {
			return (
				<Alert
					variant="light-warning"
					className="bg-light-warning text-dark-warning border-0"
					onClose={() => setShow(false)}
					dismissible
				>
					<strong>payout@geeks.com</strong>
					<p>
						Your selected payout method was confirmed on Next Payout on 15 July,
						2020
					</p>
				</Alert>
			);
		}
		return '';
	};

	// Options 1 select control values
	const options1 = [
		{ value: '30 days', label: '30 days' },
		{ value: '2 Months', label: '2 Months' },
		{ value: '6 Months', label: '6 Months' }
	];

	// Month select control values
	const months = [
		{ value: 'Oct 2020', label: 'Oct 2020' },
		{ value: 'Jan 2021', label: 'Jan 2021' },
		{ value: 'May 2021', label: 'May 2021' }
	];

	// Transaction Type control values
	const transactionType = [
		{ value: 'Cash Transactions', label: 'Cash Transactions' },
		{ value: 'Non Cash Transactions', label: 'Non Cash Transactions' },
		{ value: 'Credit Transactions', label: 'Credit Transactions' }
	];

	return (
		<ProfileLayout>
			<Card className="border-0">
				<Card.Header>
					<div className="mb-3 mb-lg-0">
						<h3 className="mb-0">Payout Method</h3>
						<p className="mb-0">
							Order Dashboard is a quick overview of all current orders.
						</p>
					</div>
				</Card.Header>
				<Card.Body>
					<AlertDismissible />
					<Row className="mt-6">
						<Col xl={4} lg={4} md={12} sm={12} className="mb-3 mb-lg-0">
							<div className="text-center">
								{/* <!-- PayOut chart --> */}
								<ApexCharts
									options={PayoutChartOptions}
									series={PayoutChartSeries}
									height={165}
									type="bar"
								/>
								<h4 className="mb-1">Your Earning this month</h4>
								<h5 className="mb-0 display-4 fw-bold">$3,210</h5>
								<p className="px-4">Update your payout method in settings</p>
								<Link to="#" className="btn btn-primary">
									Withdraw Earning
								</Link>
							</div>
						</Col>
						<Col xl={8} lg={8} md={12} sm={12}>
							{/* <!-- Check box --> */}
							<div className="border p-4 rounded-3 mb-3">
								<Form.Check>
									<Form.Check.Input
										type="radio"
										name="customRadio"
										id="default-radio1"
									/>
									<Form.Check.Label>
										<Image src={PayPal} alt="" />
									</Form.Check.Label>
								</Form.Check>
								<p>Your paypal account has been authorized for payouts.</p>
								<Link to="#" className="btn btn-outline-primary">
									Remove Account
								</Link>
							</div>
							{/* <!-- Check box --> */}
							<div className="border p-4 rounded-3 mb-3">
								<Form.Check>
									<Form.Check.Input
										type="radio"
										name="customRadio"
										id="default-radio2"
									/>
									<Form.Check.Label>
										<Image src={Payoneer} alt="" />
									</Form.Check.Label>
								</Form.Check>
							</div>
							{/* <!-- Check box --> */}
							<div className="border p-4 rounded-3">
								<Form.Check>
									<Form.Check.Input
										type="radio"
										name="customRadio"
										id="default-radio3"
									/>
									<Form.Check.Label>Bank Transfer</Form.Check.Label>
								</Form.Check>
							</div>
						</Col>
					</Row>
				</Card.Body>
			</Card>

			<Card className="border-0 mt-4">
				<Card.Header>
					<h3 className="mb-0 h4">Withdraw History</h3>
				</Card.Header>
				<Card.Body>
					<Row className="align-items-center">
						<Col lg={3} md={6} className="pe-md-0 mb-2 mb-lg-0">
							<FormSelect options={options1} placeholder="Select Option" />
						</Col>
						<Col lg={3} md={6} className="pe-md-0 mb-2 mb-2 mb-lg-0">
							<FormSelect options={months} placeholder="Months" />
						</Col>
						<Col lg={4} md={6} className="mb-2 mb-2 mb-lg-0">
							<FormSelect
								options={transactionType}
								placeholder="Transaction Type"
							/>
						</Col>
						<Col lg={2} md={6} className="text-lg-end">
							<Button variant="outline-secondary" href="#" download={true}>
								<i className="fe fe-download"></i>
							</Button>
						</Col>
					</Row>
				</Card.Body>
				<Card.Body className="p-0 pb-4">
					<Table hover responsive className="text-nowrap table-centered">
						<thead>
							{table.getHeaderGroups().map(headerGroup => (
								<tr key={headerGroup.id}>
									{headerGroup.headers.map(header => (
										<th key={header.id}>
											{header.isPlaceholder
												? null
												: flexRender(
													header.column.columnDef.header,
													header.getContext()
												)}
										</th>
									))}
								</tr>
							))}
						</thead>
						<tbody>
							{table.getRowModel().rows.map(row => (
								<tr key={row.id}>
									{row.getVisibleCells().map(cell => (
										<td key={cell.id}>
											{flexRender(cell.column.columnDef.cell, cell.getContext())}
										</td>
									))}
								</tr>
							))}

						</tbody>
					</Table>

					{/* Pagination @ Footer */}
					<div className="mt-4">
						<Pagination table={table} />
					</div>
				</Card.Body>
			</Card>
		</ProfileLayout>
	);
};

export default Payouts;
